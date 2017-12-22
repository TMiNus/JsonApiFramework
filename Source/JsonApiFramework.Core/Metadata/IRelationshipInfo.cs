// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the metadata for an individual 'relationship' on a json:api/CLR resource type.</summary>
    public interface IRelationshipInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the json:api 'rel' for the relationship.</summary>
        string ApiRel { get; }

        /// <summary>Gets the json:api 'relationship cardinality' from the source to the related resource type.</summary>
        RelationshipCardinality ApiCardinality { get; }

        /// <summary>Gets the CLR 'type' of the related resource type.</summary>
        Type ClrRelatedResourceType { get; }

        /// <summary>Gets the optional CLR property binding object of the related CLR resource object.</summary>
        IClrPropertyBinding ClrRelatedResourcePropertyBinding { get; }
        #endregion
    }
}