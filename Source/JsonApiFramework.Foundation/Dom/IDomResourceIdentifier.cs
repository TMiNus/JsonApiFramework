// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents a json:api resource identifier object.
    /// </summary>
    public interface IDomResourceIdentifier : IDomObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Gets the read-only DOM property node that represents the "type"
        /// of this json:api resource identifier object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is required, "type" is required.
        /// </remarks>
        IDomProperty DomType { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the "id"
        /// of this json:api resource identifier object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is required, "id" is required.
        /// </remarks>
        IDomProperty DomId { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// meta object of this json:api resource identifier object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, meta is optional.
        /// </remarks>
        IDomProperty DomMeta { get; }
        #endregion
    }
}