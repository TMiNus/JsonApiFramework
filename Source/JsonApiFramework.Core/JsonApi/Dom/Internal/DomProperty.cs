﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Linq;

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomProperty : DomNode
        , IDomProperty
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomProperty(string apiPropertyName)
            : this(ApiPropertyType.None, apiPropertyName)
        { }

        public DomProperty(string apiPropertyName, DomNode domPropertyValue)
            : this(ApiPropertyType.None, apiPropertyName, domPropertyValue)
        { }

        public DomProperty(ApiPropertyType apiPropertyType, string apiPropertyName)
            : base(DomNodeType.Property, apiPropertyName)
        {
            this.ApiPropertyType = apiPropertyType;
            this.ApiPropertyName = apiPropertyName;
        }

        public DomProperty(ApiPropertyType apiPropertyType, string apiPropertyName, DomNode domPropertyValue)
            : base(DomNodeType.Property, apiPropertyName, domPropertyValue)
        {
            this.ApiPropertyType = apiPropertyType;
            this.ApiPropertyName = apiPropertyName;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomProperty Implementation
        public ApiPropertyType ApiPropertyType
        {
            get => this.TryGetAttributeValue(ApiPropertyTypeAttributeName, out ApiPropertyType apiPropertyType)
                ? apiPropertyType
                : ApiPropertyType.None;
            private set
            {
                if (value == ApiPropertyType.None)
                {
                    this.RemoveAttributeValue(ApiPropertyTypeAttributeName);
                    return;
                }

                this.SetAttributeValue(ApiPropertyTypeAttributeName, value);
            }
        }

        public string ApiPropertyName { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomProperty Implementation
        public IDomNode DomPropertyValue()
        {
            return this.Nodes()
                       .Cast<IDomNode>()
                       .SingleOrDefault();
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string ApiPropertyTypeAttributeName = "property-type";
        #endregion
    }
}
