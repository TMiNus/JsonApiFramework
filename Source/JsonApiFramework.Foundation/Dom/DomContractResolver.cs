// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Dom.Internal;
using JsonApiFramework.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JsonApiFramework.Dom
{
    /// <summary>
    /// Represents the integration object needed by the JSON.NET serialization pipeline
    /// for the JSON serialization and deserialization of the DOM.
    /// </summary>
    public class DomContractResolver : DefaultContractResolver
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomContractResolver(DomJsonSerializerSettings domJsonSerializerSettings = null)
        {
            Contract.Requires(domJsonSerializerSettings != null);

            this.DomJsonSerializerSettings = domJsonSerializerSettings ?? DefaultDomJsonSerializerSettings;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Methods
        protected override JsonContract CreateContract(Type objectType)
        {
            Contract.Requires(objectType != null);

            var jsonContract = base.CreateContract(objectType);

            // Initialize contract for DOM object types.
            if (TypeToJsonContractInitializerDictionary.TryGetValue(objectType, out var contractInitializer))
            {
                contractInitializer(jsonContract, this.DomJsonSerializerSettings);
            }
            else
            {
                // Special case of the generic DomValue<TValue> type.
                if (TypeReflection.IsImplementationOf(objectType, typeof(IDomValue)))
                    InitializeDomValueContract(jsonContract, this.DomJsonSerializerSettings);
            }

            return jsonContract;
        }
        #endregion

        // PRIVATE PROPERTIES /////////////////////////////////////////////
        #region Properties
        private DomJsonSerializerSettings DomJsonSerializerSettings { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Initialize Methods
        private static void InitializeDomArrayContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomArrayConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomDataContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomDataConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomDocumentContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomDocumentConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomErrorContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomErrorConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomJsonApiContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomJsonApiConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomLinkContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomLinkConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomLinksContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomLinksConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomObjectContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomObjectConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomRelationshipContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomRelationshipConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomRelationshipsContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomRelationshipsConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomResourceIdentifierContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomResourceIdentifierConverter(domJsonSerializerSettings);
        }

        private static void InitializeDomValueContract(JsonContract jsonContract, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonContract != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonContract.Converter = new DomValueConverter(domJsonSerializerSettings);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly DomJsonSerializerSettings DefaultDomJsonSerializerSettings =
            new DomJsonSerializerSettings
                {
                    NullValueHandlingOverrides = new Dictionary<ApiPropertyType, NullValueHandling>
                        {
                            {ApiPropertyType.Meta, NullValueHandling.Ignore}
                        }
                };

        private static readonly IReadOnlyDictionary<Type, Action<JsonContract, DomJsonSerializerSettings>> TypeToJsonContractInitializerDictionary = new Dictionary<Type, Action<JsonContract, DomJsonSerializerSettings>>
            {
                { typeof(IDomArray), InitializeDomArrayContract },
                { typeof(IDomData), InitializeDomDataContract },
                { typeof(IDomDocument), InitializeDomDocumentContract },
                { typeof(IDomError), InitializeDomErrorContract },
                { typeof(IDomJsonApi), InitializeDomJsonApiContract },
                { typeof(IDomLink), InitializeDomLinkContract },
                { typeof(IDomLinks), InitializeDomLinksContract },
                { typeof(IDomObject), InitializeDomObjectContract },
                { typeof(IDomRelationship), InitializeDomRelationshipContract },
                { typeof(IDomRelationships), InitializeDomRelationshipsContract },
                { typeof(IDomResourceIdentifier), InitializeDomResourceIdentifierContract },
                { typeof(IDomValue), InitializeDomValueContract },

                { typeof(DomArray), InitializeDomArrayContract },
                { typeof(DomData), InitializeDomDataContract },
                { typeof(DomDocument), InitializeDomDocumentContract },
                { typeof(DomError), InitializeDomErrorContract },
                { typeof(DomJsonApi), InitializeDomJsonApiContract },
                { typeof(DomLink), InitializeDomLinkContract },
                { typeof(DomLinks), InitializeDomLinksContract },
                { typeof(DomObject), InitializeDomObjectContract },
                { typeof(DomRelationship), InitializeDomRelationshipContract },
                { typeof(DomRelationships), InitializeDomRelationshipsContract },
                { typeof(DomResourceIdentifier), InitializeDomResourceIdentifierContract },
            };
        #endregion
    }
}