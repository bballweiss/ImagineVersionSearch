using FluentValidation.TestHelper;
using ImagineSoftware.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImagineSoftwareTests
{
    [TestClass]
    public class SearchRequestTests
    {
        private SearchRequestValidator validator;

        [TestInitialize]
        public void Setup()
        {
            validator = new SearchRequestValidator();
        }

        [TestMethod]
        public void ShouldNotHaveErrorWhenOnlyMajorProvided()
        {
            validator.ShouldNotHaveValidationErrorFor(searchRequest => searchRequest.Input, "2");
        }

        [TestMethod]
        public void ShouldNotHaveErrorWhenMajorMinorProvided()
        {
            validator.ShouldNotHaveValidationErrorFor(searchRequest => searchRequest.Input, "1.5");
        }

        [TestMethod]
        public void ShouldNotHaveErrorWhenMajorMinorPatchProvided()
        {
            validator.ShouldNotHaveValidationErrorFor(searchRequest => searchRequest.Input, "2.12.4");
        }

        [TestMethod]
        public void ShouldHaveErrorWhenLetterIncluded()
        {
            validator.ShouldHaveValidationErrorFor(searchRequest => searchRequest.Input, "2a.3.3");
        }

        [TestMethod]
        public void ShouldHaveErrorWhenTooManyPieces()
        {
            validator.ShouldHaveValidationErrorFor(searchRequest => searchRequest.Input, "2.3.3.4");
        }

        [TestMethod]
        public void ShouldHaveErrorWhenBlankPiece()
        {
            validator.ShouldHaveValidationErrorFor(searchRequest => searchRequest.Input, "2..4");
        }
    }
}
