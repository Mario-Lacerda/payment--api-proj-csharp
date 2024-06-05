using Xunit;
using System;
using System.Collections.Generic;
using Payment_API.src.DTOs;
using Payment_API.src.Models;

namespace Payment_API.Tests
{
    public class MappingProfileTest : MapperConfigurationTest
    {
        private readonly Random random = new();

        [Fact]
        public void AutoMapper_ValidateMapperConfigurationTest()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_MapSaleToSaleDTO_CorrectMapping()
        {
            var sale = MockSale();

            var result = _mapper.Map<SaleDTO>(sale);

            Assert.NotNull(result);
            Assert.IsType<SaleDTO>(result);
            Assert.Equal("Aguardando pagamento", result.Status);
            Assert.Equal("Nome Válido", result.Seller.Name);
        }

        [Fact]
        public void Map_MapSellerToSellerDTO_CorrectMapping()
        {
            var seller = new Seller();
            
            var result = _mapper.Map<SellerDTO>(seller);

            Assert.NotNull(result);
            Assert.IsType<SellerDTO>(result);
        }

        [Fact]
        public void Map_MapNewSaleDTO_CorrectMappingToSale()
        {
            var newSaleDTO = MockNewSaleDTO();
            
            var result = _mapper.Map<Sale>(newSaleDTO);

            Assert.NotNull(result);
            Assert.IsType<Sale>(result);
            Assert.Equal(EnumStatus.Aguardando, result.Status);
        }

         [Fact]
        public void Map_MapEnumStatusUpdateDTO_CorrectMappingToEnumStatus()
        {
            var enumUpdate = EnumStatusUpdateDTO.Cancelada;
            
            var result = _mapper.Map<EnumStatus>(enumUpdate);

            Assert.IsType<EnumStatus>(result);
            Assert.Equal(EnumStatus.Cancelada, result);
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