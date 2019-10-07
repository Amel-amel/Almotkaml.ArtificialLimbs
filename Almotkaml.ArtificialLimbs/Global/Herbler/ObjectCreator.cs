using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    public static class ObjectCreator
    {
        private delegate T ObjectActivator<out T>(params object[] args);

        private static ObjectActivator<T> GetActivator<T>
            (ConstructorInfo ctor)
        {
            var paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            var param =
                Expression.Parameter(typeof(object[]), "args");

            var argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (var i = 0; i < paramsInfo.Length; i++)
            {
                var index = Expression.Constant(i);
                var paramType = paramsInfo[i].ParameterType;

                var paramAccessorExp =
                    Expression.ArrayIndex(param, index);

                var paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            var newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            var lambda =
                Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //compile it
            var compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }


        public static T Create<T>(Type type, params object[] args)
        {
            Check.NotNull(type, nameof(type));

            var ctor = type.GetTypeInfo().DeclaredConstructors.First();
            var createdActivator = GetActivator<T>(ctor);
            T instance;

            try
            {
                instance = createdActivator(args);
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new Exception(exception.Message + " , constructors doesn't match with the supplied arguments", exception);
            }

            if (instance == null)
                throw new NullReferenceException("failed to create object of type : " + type);

            return instance;
        }
    }
}