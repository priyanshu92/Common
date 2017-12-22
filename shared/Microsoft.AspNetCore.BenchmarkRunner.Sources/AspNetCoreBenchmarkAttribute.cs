// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess;

namespace BenchmarkDotNet.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly)]
    public class AspNetCoreBenchmarkAttribute : Attribute, IConfigSource
    {
        public static bool UseValidationConfig { get; set; }

        public Type ConfigType { get;set; }
        public Type ValidationConfigType { get;set; }

        public AspNetCoreBenchmarkAttribute() : this(typeof(CoreConfig))
        {
        }

        public AspNetCoreBenchmarkAttribute(Type configType) : this(configType, typeof(CoreValidationConfig))
        {
        }

        public AspNetCoreBenchmarkAttribute(Type configType, Type validationConfigType)
        {
            ConfigType = configType;
            ValidationConfigType = validationConfigType;
        }

        public IConfig Config => (IConfig) Activator.CreateInstance(UseValidationConfig ? ValidationConfigType : ConfigType, Array.Empty<object>());
    }
}
