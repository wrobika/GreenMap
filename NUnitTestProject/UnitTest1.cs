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
            ActionResult<Dictionary<long?, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(707));
            Assert.IsFalse(result.Value.ContainsKey(705));
        }

        [Test]
        public async Task SearchByDistrict()
        {
            OdwiertSearch preferences = new OdwiertSearch { Lokalizacja = "7" };
            ActionResult<Dictionary<long?, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(701));
            Assert.IsFalse(result.Value.ContainsKey(707));
        }

        [Test]
        public async Task SearchByDepth()
        {
            OdwiertSearch preferences = new OdwiertSearch { GlebokoscZwierciadla1 = -2, GlebokoscZwierciadla2 = -1 };
            ActionResult<Dictionary<long?, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(51));
            Assert.IsFalse(result.Value.ContainsKey(50));
        }

        [Test]
        public async Task SearchByFiltering()
        {
            OdwiertSearch preferences = new OdwiertSearch { Filtracja1 = (decimal)0.001, Filtracja2 = (decimal)0.01 };
            ActionResult<Dictionary<long?, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(101));
            Assert.IsFalse(result.Value.ContainsKey(50));
        }

        [Test]
        public async Task SearchByY()
        {
            OdwiertSearch preferences = new OdwiertSearch { EurefY1 = 575071, EurefY2 = 575072 };
            ActionResult<Dictionary<long?, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(608));
            Assert.IsFalse(result.Value.ContainsKey(688));
        }

        [Test]
        public async Task SearchByX()
        {
            OdwiertSearch preferences = new OdwiertSearch { EurefX1 = 241669, EurefX2 = 241670 };
            ActionResult<Dictionary<long?, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(707));
            Assert.IsFalse(result.Value.ContainsKey(706));
        }

        [Test]
        public async Task SearchByRbdh()
        {
            OdwiertSearch preferences = new OdwiertSearch { NrRbdh = 9730234 };
            ActionResult<Dictionary<long?, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(50));
            Assert.IsFalse(result.Value.ContainsKey(123));
        }

        [Test]
        public async Task SearchByName()
        {
            OdwiertSearch preferences = new OdwiertSearch { NazwaObiektu = "Odwodnienie SA-4" };
            ActionResult<Dictionary<long?, string>> result = await _searchController.SetPreferencesAndSearch(preferences);
            Assert.IsTrue(result.Value.ContainsKey(703));
            Assert.IsFalse(result.Value.ContainsKey(701));
        }
    }
}