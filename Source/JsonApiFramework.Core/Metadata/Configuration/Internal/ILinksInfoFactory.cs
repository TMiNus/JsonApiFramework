// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Metadata.Conventions;

namespace JsonApiFramework.Metadata.Configuration.Internal
{
    internal interface ILinksInfoFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        ILinksInfo Create(IMetadataConventions metadataConventions);
        #endregion
    }
}