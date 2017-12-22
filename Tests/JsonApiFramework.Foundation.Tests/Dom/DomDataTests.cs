﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Dom;
using JsonApiFramework.Dom.Internal;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Tests.JsonApi.Dom;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Dom
{
    public class DomDataTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomDataTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomJsonSerializationTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationTest)
        {
            var data = domJsonSerializationTest.Data;
            var factory = domJsonSerializationTest.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomJsonSerializationTestData))]
        public void TestJsonDeserialize(DomJsonSerializationUnitTestFactory domJsonSerializationTest)
        {
            var data = domJsonSerializationTest.Data;
            var factory = domJsonSerializationTest.DomJsonDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly DomJsonSerializerSettings TestDomJsonSerializerSettings = new DomJsonSerializerSettings
            {
                NullValueHandlingOverrides = null
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        public static readonly IEnumerable<object[]> DomJsonSerializationTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id"),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes"),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles"))),
@"{
  ""type"": ""articles""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id"),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes"),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": null,
  ""attributes"": null,
  ""relationships"": null,
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes"),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42"))),
@"{
  ""type"": ""articles"",
  ""id"": ""42""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes"),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": null,
  ""relationships"": null,
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!"))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships"),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": null,
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            ))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
    ""author"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/author"",
        ""related"": ""https://api.example.com/articles/42/author""
      },
      ""data"": {
        ""type"": ""people"",
        ""id"": ""42""
      }
    },
    ""comments"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/comments"",
        ""related"": ""https://api.example.com/articles/42/comments""
      },
      ""data"": [
        {
          ""type"": ""comments"",
          ""id"": ""68""
        },
        {
          ""type"": ""comments"",
          ""id"": ""86""
        }
      ]
    }
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
    ""author"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/author"",
        ""related"": ""https://api.example.com/articles/42/author""
      },
      ""data"": {
        ""type"": ""people"",
        ""id"": ""42""
      }
    },
    ""comments"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/comments"",
        ""related"": ""https://api.example.com/articles/42/comments""
      },
      ""data"": [
        {
          ""type"": ""comments"",
          ""id"": ""68""
        },
        {
          ""type"": ""comments"",
          ""id"": ""86""
        }
      ]
    }
  },
  ""links"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndLinksAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42"))))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
    ""author"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/author"",
        ""related"": ""https://api.example.com/articles/42/author""
      },
      ""data"": {
        ""type"": ""people"",
        ""id"": ""42""
      }
    },
    ""comments"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/comments"",
        ""related"": ""https://api.example.com/articles/42/comments""
      },
      ""data"": [
        {
          ""type"": ""comments"",
          ""id"": ""68""
        },
        {
          ""type"": ""comments"",
          ""id"": ""86""
        }
      ]
    }
  },
  ""links"": {
    ""self"":  ""https://api.example.com/articles/42""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndLinksAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
    ""author"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/author"",
        ""related"": ""https://api.example.com/articles/42/author""
      },
      ""data"": {
        ""type"": ""people"",
        ""id"": ""42""
      }
    },
    ""comments"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/comments"",
        ""related"": ""https://api.example.com/articles/42/comments""
      },
      ""data"": [
        {
          ""type"": ""comments"",
          ""id"": ""68""
        },
        {
          ""type"": ""comments"",
          ""id"": ""86""
        }
      ]
    }
  },
  ""links"": {
    ""self"":  ""https://api.example.com/articles/42""
  },
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndTypeAndIdAndAttributesAndRelationshipsAndLinksAndMeta",
                                TestJsonSerializerSettings,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my bikeshed!")))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                        new DomObject(
                                            new DomProperty("version", new DomValue<string>("2.0"))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my bikeshed!""
  },
  ""relationships"": {
    ""author"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/author"",
        ""related"": ""https://api.example.com/articles/42/author""
      },
      ""data"": {
        ""type"": ""people"",
        ""id"": ""42""
      }
    },
    ""comments"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/comments"",
        ""related"": ""https://api.example.com/articles/42/comments""
      },
      ""data"": [
        {
          ""type"": ""comments"",
          ""id"": ""68""
        },
        {
          ""type"": ""comments"",
          ""id"": ""86""
        }
      ]
    }
  },
  ""links"": {
    ""self"":  ""https://api.example.com/articles/42""
  },
  ""meta"":  {
    ""version"": ""2.0""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceAndAllJsonTypesAttributes",
                                TestJsonSerializerSettings,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Attributes, "attributes",
                                        new DomObject(
                                            new DomProperty("title", new DomValue<string>("JSON API paints my house!")),
                                            new DomProperty("is-online", new DomValue<bool>(true)),
                                            new DomProperty("version", new DomValue<decimal>(1.42m)),
                                            new DomProperty("line_count", new DomValue<int>(1024)),
                                            new DomProperty("tags", new DomArray(
                                                new DomItem(0, new DomValue<string>("json")),
                                                new DomItem(1, new DomValue<string>("json:api")),
                                                new DomItem(2, new DomValue<string>("bikeshed")),
                                                new DomItem(3, new DomValue<string>("REST")))),
                                            new DomProperty("audit",
                                                new DomObject(
                                                    new DomProperty("created",
                                                        new DomObject(
                                                            new DomProperty("date", new DomValue<DateTimeOffset>(new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)))),
                                                            new DomProperty("by", new DomValue<string>("John Doe")))),
                                                    new DomProperty("modified",
                                                        new DomObject(
                                                            new DomProperty("date", new DomValue<DateTimeOffset>(new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) + new TimeSpan(42, 0, 42, 42))),
                                                            new DomProperty("by", new DomValue<string>("Jane Doe")))),
                                                    new DomProperty("modified-history", new DomArray(
                                                        new DomItem(0,
                                                            new DomObject(
                                                                new DomProperty("date", new DomValue<DateTimeOffset>(new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) + new TimeSpan(1, 0, 10, 20))),
                                                                new DomProperty("by", new DomValue<string>("Jane Doe")))),
                                                        new DomItem(1,
                                                            new DomObject(
                                                                new DomProperty("date", new DomValue<DateTimeOffset>(new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) + new TimeSpan(2, 0, 15, 59))),
                                                                new DomProperty("by", new DomValue<string>("Jane Doe")))),
                                                        new DomItem(2,
                                                            new DomObject(
                                                                new DomProperty("date", new DomValue<DateTimeOffset>(new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) + new TimeSpan(3, 0, 24, 24))),
                                                                new DomProperty("by", new DomValue<string>("Jane Doe")))),
                                                        new DomItem(3,
                                                            new DomObject(
                                                                new DomProperty("date", new DomValue<DateTimeOffset>(new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) + new TimeSpan(42, 0, 42, 42))),
                                                                new DomProperty("by", new DomValue<string>("Jane Doe")))))),
                                                    new DomProperty("deleted",
                                                        new DomObject(
                                                            new DomProperty("date", new DomValue<DateTimeOffset>(new DateTimeOffset(1968, 5, 20, 20, 2, 0, new TimeSpan(-4, 0, 0)) + new TimeSpan(1000, 0, 0, 0))),
                                                            new DomProperty("by", new DomValue<string>("Jane Doe")))))))),
                                    new DomProperty(ApiPropertyType.Relationships, "relationships",
                                        new DomRelationships(
                                            new DomProperty(ApiPropertyType.Relationship, "author",
                                                new DomRelationship(RelationshipType.ToOneRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/author")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/author")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomResourceIdentifier(
                                                            new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("people")),
                                                            new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")))))),
                                            new DomProperty(ApiPropertyType.Relationship, "comments",
                                                new DomRelationship(RelationshipType.ToManyRelationship,
                                                    new DomProperty(ApiPropertyType.Links, "links",
                                                        new DomLinks(
                                                            new DomProperty(ApiPropertyType.Link, "self",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/relationships/comments")))),
                                                            new DomProperty(ApiPropertyType.Link, "related",
                                                                new DomLink(new DomProperty(ApiPropertyType.HRef, "href",
                                                                    new DomValue<string>("https://api.example.com/articles/42/comments")))))),
                                                    new DomProperty(ApiPropertyType.Data, "data",
                                                        new DomArray(
                                                            new DomItem(0,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("68")))),
                                                            new DomItem(1,
                                                                new DomResourceIdentifier(
                                                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("comments")),
                                                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("86"))))))))
                                            )),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "self",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")))))),
                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                        new DomObject(
                                            new DomProperty("version", new DomValue<string>("2.0"))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""attributes"": {
    ""title"": ""JSON API paints my house!"",
    ""is-online"": true,
    ""version"": 1.42,
    ""line_count"": 1024,
    ""tags"": [
      ""json"",
      ""json:api"",
      ""bikeshed"",
      ""REST""
    ],
    ""audit"": {
      ""created"": {
        ""date"": ""1968-05-20T20:02:00-04:00"",
        ""by"": ""John Doe""
      },
      ""modified"": {
        ""date"": ""1968-07-01T20:44:42-04:00"",
        ""by"": ""Jane Doe""
      },
      ""modified-history"": [
        {
          ""date"": ""1968-05-21T20:12:20-04:00"",
          ""by"": ""Jane Doe""
        },
        {
          ""date"": ""1968-05-22T20:17:59-04:00"",
          ""by"": ""Jane Doe""
        },
        {
          ""date"": ""1968-05-23T20:26:24-04:00"",
          ""by"": ""Jane Doe""
        },
        {
          ""date"": ""1968-07-01T20:44:42-04:00"",
          ""by"": ""Jane Doe""
        }
      ],
      ""deleted"": {
        ""date"": ""1971-02-14T20:02:00-04:00"",
        ""by"": ""Jane Doe""
      }
    }
  },
  ""relationships"": {
    ""author"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/author"",
        ""related"": ""https://api.example.com/articles/42/author""
      },
      ""data"": {
        ""type"": ""people"",
        ""id"": ""42""
      }
    },
    ""comments"": {
      ""links"": {
        ""self"": ""https://api.example.com/articles/42/relationships/comments"",
        ""related"": ""https://api.example.com/articles/42/comments""
      },
      ""data"": [
        {
          ""type"": ""comments"",
          ""id"": ""68""
        },
        {
          ""type"": ""comments"",
          ""id"": ""86""
        }
      ]
    }
  },
  ""links"": {
    ""self"":  ""https://api.example.com/articles/42""
  },
  ""meta"":  {
    ""version"": ""2.0""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceIdentifierAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42"))),
@"{
  ""type"": ""articles"",
  ""id"": ""42""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceIdentifierAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomData>(x),
                            x => new DomJsonDeserializeUnitTest<IDomData>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithArticleResourceIdentifierAndMeta",
                                TestJsonSerializerSettings,
                                new DomData(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("version", new DomValue<string>("2.0"))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""meta"":  {
    ""version"": ""2.0""
  }
}"))
                    },

        };
        #endregion
    }
}
