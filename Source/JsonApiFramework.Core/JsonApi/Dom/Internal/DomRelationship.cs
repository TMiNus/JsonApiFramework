﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomRelationship : DomObject
        , IDomRelationship
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomRelationship(RelationshipType apiRelationshipType, params DomProperty[] domProperties)
            : this(apiRelationshipType, domProperties.AsEnumerable())
        { }

        public DomRelationship(RelationshipType apiRelationshipType, IEnumerable<DomProperty> domProperties)
            : base("relationship object", domProperties)
        {
            this.ApiRelationshipType = apiRelationshipType;

            foreach (var domProperty in this.DomProperties())
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
                    case ApiPropertyType.Links:
                        this.DomLinks = domProperty;
                        break;

                    case ApiPropertyType.Data:
                        this.DomData = domProperty;
                        break;

                    case ApiPropertyType.Meta:
                        this.DomMeta = domProperty;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomRelationship Implementation
        public RelationshipType ApiRelationshipType
        {
            get => this.GetAttributeValue<RelationshipType>(ApiRelationshipTypeAttributeName);
            private set => this.SetAttributeValue(ApiRelationshipTypeAttributeName, value);
        }

        public IDomProperty DomLinks { get; }

        public IDomProperty DomData { get; }

        public IDomProperty DomMeta { get; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string ApiRelationshipTypeAttributeName = "relationship-type";
        #endregion
    }
}
