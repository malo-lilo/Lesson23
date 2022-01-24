﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace DemoApiTesting.ContractTests
{
    public class GetPlanetsContractTests : ContractBase
    {
        private const string Host = "https://swapi.dev/api";

        [Test]
       
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
       
        public async Task CheckContractGetPlanetPositiveTesting(int page)
        {
            string Api = $"/planets/?page={page}";
            var client = new HttpClient() ;
            var response = await client.GetAsync(new Uri(Host + Api), new CancellationToken());
            
            JSchema schema = JSchema.Parse(GetFileAsString("getPlanets.Positive.json"));
            await CheckValidationResponseMessageBySchemaAsync(response, schema);
        }
        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(20)]
        public async Task CheckContractGetPlanetNegativeTesting(int page)
        {
            string Api = $"/planets/?page={page}";
            var client = new HttpClient() ;
            var response = await client.GetAsync(new Uri(Host + Api), new CancellationToken());
            
            JSchema schema = JSchema.Parse(GetFileAsString("getPlanets.Negative.json"));
            await CheckValidationResponseMessageBySchemaAsync(response, schema);
        }
        
    }
}