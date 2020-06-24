using NUnit.Framework;
using GreenMap.Models;
using GreenMap.Controllers;
using GreenMap;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;

namespace NUnitTestProject
{
    public class Tests
    {
        private readonly epionierContext _context = new epionierContext();
        private SearchController _searchController;
        private OdwiertController _odwiertController;

        [SetUp]
        public void Setup()
        {
            _searchController = new SearchController(_context);
            _odwiertController = new OdwiertController(_context);
        }

        [Test]
        public async Task SearchByStatus()
        {
            OdwiertSearch preferences = new OdwiertSearch { Status = "Monitoringowy" };
            ActionResult<Dictionary<long, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(707));
            Assert.IsFalse(result.Value.ContainsKey(705));
        }

        [Test]
        public async Task SearchByDistrict()
        {
            OdwiertSearch preferences = new OdwiertSearch { Lokalizacja = "7" };
            ActionResult<Dictionary<long, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(701));
            Assert.IsFalse(result.Value.ContainsKey(707));
        }

        [Test]
        public void SearchByDepth()
        {
            
        }

        [Test]
        public void SearchByFiltering()
        {
            
        }

        [Test]
        public void SearchByY()
        {
            
        }

        [Test]
        public void SearchByX()
        {
            
        }

        [Test]
        public void SearchByRbdh()
        {
            
        }

        [Test]
        public void SearchByName()
        {
           
        }
    }
}