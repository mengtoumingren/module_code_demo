using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InjectContainer
{
    public class Container
    {
        private static Dictionary<string, Type> dicToInstances = null;
        private static Dictionary<string, List<Type>> dicReturnTypeInfo = null;
        private static object objLock = null;
        private static Container container = null;

        private Container() { }

        static Container()
        {
            container = new Container();
            dicToInstances = new Dictionary<string, Type>();
            dicReturnTypeInfo = new Dictionary<string, List<Type>>();
            objLock = new object();
        }
        public static  Container GetContainer()
        {
            return container;
        }
        #region 重载注册器

        /// <summary>
        /// 接口注册
        /// </summary>
        /// <param name="toNameSpace">目标程序集命名空间</param>
        public void Register(string toNameSpace)
        {
            var toAssembly = Assembly.Load(toNameSpace);
            var types = toAssembly.GetTypes();
            Register(types);
        }
        /// <summary>
        /// 接口注册
        /// </summary>
        /// <param name="types">类型数组</param>
        public void Register(params Type[] types)
        {
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    if (dicToInstances.ContainsKey(inter.FullName)) continue;
                    dicToInstances.Add(inter.FullName, type);
                }
            }
        }
        /// <summary>
        /// 接口注册
        /// </summary>
        /// <typeparam name="TFrom">来源类型</typeparam>
        /// <typeparam name="TTo">目标类型</typeparam>
        public void Register<TFrom, TTo>()
        {
            Register(typeof(TFrom), typeof(TTo));
        }
        /// <summary>
        /// 接口注册
        /// </summary>
        /// <param name="fromType">来源类型</param>
        /// <param name="toType">目标类型</param>
        public void Register(Type fromType, Type toType)
        {
            dicToInstances.Add(fromType.FullName, toType);
        }
        #endregion
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetInstance<T>()
        {
            return GetInstance<T>(typeof(T));
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetInstance<T>(Type type)
        {
            if (type.IsInterface)
            {
                if (dicToInstances.ContainsKey(type.FullName))
                    return GetInstance<T>(dicToInstances[type.FullName]);
                else
                    return default(T);
            }
            else
            {
                return CreateInstance<T>(type);
            }
        }
        private T CreateInstance<T>(Type type)
        {
            List<Type> typesOfParameter = new List<Type>();
            if (dicReturnTypeInfo.ContainsKey(type.FullName))
            {
                //如果有类型数据就不需要再获取一次了
                typesOfParameter = dicReturnTypeInfo[type.FullName];
            }
            else
            {
                lock (objLock)
                {
                    if (!dicReturnTypeInfo.ContainsKey(type.FullName))
                    {
                        //构造函数注入
                        ConstructorInfo constructor = null;
                        var ConstructorsInfo = type.GetConstructors();
                        if (ConstructorsInfo.Count() > 0)
                        {
                            var dicCountParameters = new Dictionary<int, ParameterInfo[]>();
                            foreach (var Constructor in ConstructorsInfo)
                            {
                                var tempParameters = Constructor.GetParameters();
                                dicCountParameters.Add(tempParameters.Count(), tempParameters);
                                if (Constructor.GetCustomAttribute(typeof(ConstructorInjectAttribute)) != null)
                                {
                                    //TODO  将取出来的属性保存下来，下次用到就不用遍历了
                                    constructor = Constructor;
                                    break;
                                }
                            }
                            //如果没有指定特性，则默认取参数最多的一个
                            var parameters = constructor==null? dicCountParameters.OrderByDescending(c=>c.Key).FirstOrDefault().Value : constructor.GetParameters();

                            foreach (var item in parameters)
                            {
                                Type fromType = item.ParameterType;
                                typesOfParameter.Add(fromType);
                            }
                            dicReturnTypeInfo.Add(type.FullName, typesOfParameter);
                        }
                    }
                }
            }
            List<object> param = new List<object>();
            foreach (var pType in typesOfParameter)
            {
                if (dicToInstances.ContainsKey(pType.FullName))
                    param.Add(GetInstance<object>(dicToInstances[pType.FullName]));
                else
                    throw new Exception($"指定类型未注册:{pType.FullName}");
            }
            T t = default(T);
            if (param.Count > 0)
                t = (T)Activator.CreateInstance(type, param.ToArray());
            else
                t = (T)Activator.CreateInstance(type);
            //属性注入
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                //TODO  将取出来的属性保存下来，下次用到就不用遍历了
                var attribute = property.GetCustomAttribute(typeof(PropertyInjectAttribute));
                if (attribute != null)
                    property.SetValue(t, GetInstance<object>(property.PropertyType));
            }
            //字段注入
            var filds = type.GetFields();
            foreach (var fild in filds)
            {
                //TODO  将取出来的属性保存下来，下次用到就不用遍历了
                var attribute = fild.GetCustomAttribute(typeof(FieldInjectAttribute));
                if (attribute != null)
                    fild.SetValue(t, GetInstance<object>(fild.FieldType));
            }
            return t;
        }
    }
}
