﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if(!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception("Bu veri türünü kullanamazsınız.");
            }
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//validator nesnesi oluşturuldu.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//validate edilecek nesnenin türüne ulaşıldı.
            var entities = invocation.Arguments.Where(a => a.GetType() == entityType).ToList();//validate edilecek nesnelerin listesi

            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator,entity);
            }
        }
    }
}
