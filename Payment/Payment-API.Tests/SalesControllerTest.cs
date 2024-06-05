using Xunit;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Payment_API.src.Controllers;
using Payment_API.src.DTOs;
using Payment_API.src.Models;
using Payment_API.src.Models.Interfaces;

namespace Payment_API.Tests
{
    public class SalesControllerTest : MapperConfigurationTest
    {
        private readonly SalesController _controller;
        private readonly Mock<ISaleService> _serviceStub;
        private readonly Random random = new();

        public SalesControllerTest()
        {
            _serviceStub = new Mock<ISaleService>();
            _controller = new SalesController(_serviceStub.Object, _mapper);
        }

        [Fact]
        public void GetById_ActionExecutes_ReturnsActionResultForGet()
        {
            var result = _controller.GetById(It.IsAny<int>());

            Assert.IsType<ActionResult<SaleDTO>>(result);
        }

        [Fact]
        public void GetById_NotExistingId_ReturnsNotFound()
        {
            _serviceStub.Setup(s => s.GetById(It.IsAny<int>())).Returns((Sale)null);

            var result = _controller.GetById(It.IsAny<int>());

            Assert.Null(result.Value);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsExpectedSaleDTO()
        {
            var sale = MockSale();

            _serviceStub.Setup(s => s.GetById(sale.Id)).Returns(sale);

            var result = _controller.GetById(sale.Id);

            Assert.IsType<SaleDTO>(result.Value);
            Assert.Equal(sale.Id, result.Value.Id);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Products", "Required");

            _serviceStub.Setup(x => x.Create(It.IsAny<Sale>())).Returns(It.IsAny<Sale>());

            var newSaleDTO = MockNewSaleDTO();

            var result = _controller.Create(newSaleDTO);

            var test = Assert.IsType<BadRequestResult>(result);
            _serviceStub.Verify(x => x.Create(It.IsAny<Sale>()), Times.Never);
        }

        [Fact]
        public void Create_ValidObject_ReturnCreatedResponse()
        {
            var newSaleDTO = MockNewSaleDTO();

            var result = _controller.Create(newSaleDTO);
            
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void Create_ValidObject_ReturnCreatedSale()
        {
            Sale sale = null;

            _serviceStub.Setup(s => s.Create(It.IsAny<Sale>())).Callback<Sale>(x => sale = x);

            var newSaleDTO = MockNewSaleDTO();

            _controller.Create(newSaleDTO);

            _serviceStub.Verify(x => x.Create(It.IsAny<Sale>()), Times.Once);
            Assert.Equal(sale.Products, newSaleDTO.Products);
            Assert.Equal(sale.Seller.CPF, newSaleDTO.Seller.CPF);
            Assert.Equal(sale.Seller.Name, newSaleDTO.Seller.Name);
        }

        [Fact]
        public void UpdateStatus_NotExistingID_ReturnNotFound()
        {
            _serviceStub.Setup(s => s.GetById(It.IsAny<int>())).Returns((Sale)null);

            var result = _controller.UpdateStatus(It.IsAny<int>(), EnumStatusUpdateDTO.Aprovado);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UpdateStatus_ExistingSale_ReturnNoContentResponse()
        {
            var sale = MockSale();
            _serviceStub.Setup(s => s.GetById(sale.Id)).Returns(sale);
            _serviceStub.Setup(s => s.UpdateStatus(sale.Id, EnumStatus.Aprovado));

            var result = _controller.UpdateStatus(sale.Id, EnumStatusUpdateDTO.Aprovado);
            
            Assert.IsType<NoContentResult>(result);
            _serviceStub.Verify(s => s.UpdateStatus(sale.Id, EnumStatus.Aprovado), Times.Once);
        }
        private Sale MockSale()
        {
            int id = random.Next(1,10);

            var seller = new Seller();
            seller.Id = id;
            seller.Name = "Nome Válido";
            seller.CPF = "111.111.111-11";
            seller.Email = "nome@mail.com";
            seller.Telephone = "99-99999-9999";

            var product = new Product() { Id = 1, Item = "Produto" };

            var sale = new Sale();
            sale.Id = id;
            sale.Seller = seller;
            sale.Products = new List<Product>() { product };
            sale.Created = DateTime.Now;
            sale.Status = EnumStatus.Aguardando;

            return sale;
        }

        private NewSaleDTO MockNewSaleDTO()
        {
            int id = random.Next(1,10);

            var sellerDTO = new SellerDTO();
            sellerDTO.Id = id;
            sellerDTO.Name = "Nome Válido";
            sellerDTO.CPF = "111.111.111-11";
            sellerDTO.Email = "nome@mail.com";
            sellerDTO.Telephone = "99-99999-9999";

            var product = new Product() { Id = 1, Item = "Produto" };

            var newSaleDTO = new NewSaleDTO();
            newSaleDTO.Seller = sellerDTO;
            newSaleDTO.Products = new List<Product>() { product };
            
            return newSaleDTO;
        }
    }
}