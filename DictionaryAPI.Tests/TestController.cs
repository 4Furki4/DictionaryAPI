using DictionaryAPI.Context;
using DictionaryAPI.Controllers;
using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;
using DictionaryAPI.Operations.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;


namespace DictionaryAPI.Tests
{
    
    public class TestController
    {

        [Fact]
        void trim()
        {

            // Arrange
            string makeFirstCapitilized(string value)
            {
                return char.ToUpper(value[0], new CultureInfo("tr-TR", false)) + value.Substring(1).ToLower(new CultureInfo("tr-TR", false));
            }
            string wordName1, wordName2, wordName3, wordName4, wordName5;

            wordName1 = "Liyakat";
            wordName2 = "liyakat";
            wordName3 = "hilkat garibesi";
            wordName4 = "Hilkat garibesi";

            // Act
            wordName2 = makeFirstCapitilized(wordName2);
            wordName3 = makeFirstCapitilized(wordName3);

            // Assert

            Assert.Equal(wordName1, wordName2);

            Assert.Equal(wordName3, wordName4);
        }
    }
}
