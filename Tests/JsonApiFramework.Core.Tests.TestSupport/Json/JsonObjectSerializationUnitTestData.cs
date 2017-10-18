﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.Json
{
    public class JsonObjectSerializationUnitTestData
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public JsonObjectSerializationUnitTestData(string name, JsonSerializerSettings settings, object expectedObject, string expectedJson)
        {
            this.Name = name;
            this.Settings = settings;
            this.ExpectedSerializeObject = expectedObject;
            this.ExpectedDeserializeObject = expectedObject;
            this.ExpectedJson = expectedJson;
        }

        public JsonObjectSerializationUnitTestData(string name, JsonSerializerSettings settings, object expectedSerializeObject, object expectedDeserializeObject, string expectedJson)
        {
            this.Name = name;
            this.Settings = settings;
            this.ExpectedSerializeObject = expectedSerializeObject;
            this.ExpectedDeserializeObject = expectedDeserializeObject;
            this.ExpectedJson = expectedJson;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region User Supplied Properties
        public string Name { get; }
        public JsonSerializerSettings Settings { get; }
        public object ExpectedSerializeObject { get; }
        public object ExpectedDeserializeObject { get; }
        public string ExpectedJson { get; }
        #endregion
    }
}
