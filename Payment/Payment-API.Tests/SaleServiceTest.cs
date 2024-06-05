using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using Payment_API.src.Models;
using Payment_API.src.Models.Interfaces;
using Payment_API.src.Services;

namespace Payment_API.Tests;

public class SaleServiceTest
{
    private readonly Mock<ISaleRepository> _repositoryStub;
    private readonly Mock<IUnitOfWork> _unitOfWorkStub;
    private readonly ISaleService _service;
    private readonly Random random = new();

    public SaleServiceTest()
    {
        _repositoryStub = new Mock<ISaleRepository>();
        _unitOfWorkStub = new Mock<IUnitOfWork>();
        _service = new SaleService(_repositoryStub.Object, _unitOfWorkStub.Object);
    }

    [Fact]
    public void GetById_IdInvalid_ReturnNull()
    {
        _repositoryStub.Setup(r => r.FindById(It.IsAny<int>())).Returns((Sale)null);

        var result = _service.GetById(It.IsAny<int>());

        Assert.Null(result);
    }

    [Fact]
    public void GetById_ExistsID_ReturnSale()
    {
        var sale = MockSale();
        _repositoryStub.Setup(r => r.FindById(sale.Id)).Returns(sale);

        var result = _service.GetById(sale.Id);

        Assert.NotNull(result);
        Assert.Equal("Nome Vendedor", result.Seller.Name);
    }

    [Fact]
    public void Create_ErrorSaving_ThrowsException()
    {
        var sale = MockSale();
        _repositoryStub.Setup(r => r.Add(sale));
        _unitOfWorkStub.Setup(s => s.Complete()).Throws(new InvalidOperationException());

        Action result = () => _service.Create(sale);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(result);
        Assert.Equal("Ocorreu um erro durante o registro da venda.", exception.Message);
    }

    [Fact]
    public void Create_CriarNewSale_ReturnNewSale()
    {
        var sale = MockSale();
        _repositoryStub.Setup(r => r.Add(sale));
        _unitOfWorkStub.Setup(s => s.Complete());

        var result = _service.Create(sale);

        Assert.NotNull(result);
        Assert.IsType<Sale>(result);
        _repositoryStub.Verify(r => r.Add(sale), Times.Once);
        _unitOfWorkStub.Verify(s => s.Complete(), Times.Once);
    }

    [Fact]
    public void UpdateStatus_IdInvalid_ReturnException()
    {
        _repositoryStub.Setup(r => r.FindById(It.IsAny<int>())).Returns((Sale)null);

        Action result = () => _service.UpdateStatus(It.IsAny<int>(), EnumStatus.Aprovado);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(result);
        Assert.Equal("Este registro de venda não existe.", exception.Message);
    }

    [Fact]
    public void UpdateStatus_ErrorSaving_ThrowsException()
    {
        var sale = MockSale();
        _repositoryStub.Setup(r => r.FindById(sale.Id)).Returns(sale);
        
        _repositoryStub.Setup(r => r.Update(sale));
        _unitOfWorkStub.Setup(s => s.Complete()).Throws(new InvalidOperationException());

        Action result = () => _service.UpdateStatus(sale.Id, EnumStatus.Aprovado);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(result);
        Assert.Equal("Ocorreu um erro durante a atualização.", exception.Message);
    }

    [Fact]
    public void UpdateStatus_UpdateToValidStatus_SaveUpdatedSale()
    {
        var sale = MockSale();
        _repositoryStub.Setup(r => r.FindById(sale.Id)).Returns(sale);
        
        _repositoryStub.Setup(r => r.Update(sale));
        _unitOfWorkStub.Setup(s => s.Complete());

        _service.UpdateStatus(sale.Id, EnumStatus.Aprovado);

        Assert.True(sale.Status == EnumStatus.Aprovado);
        _repositoryStub.Verify(r => r.Update(sale), Times.Once);
        _unitOfWorkStub.Verify(s => s.Complete(), Times.Once);
    }

    private Sale MockSale()
    {
        int id = random.Next(1, 10);

        var seller = new Seller();
        seller.Id = id;
        seller.Name = "Nome Vendedor";
        seller.CPF = "000.000.000-00";
        seller.Email = "nome@mail.com";
        seller.Telephone = "99-99999-9999";

        var product = new Product() { Id = id, Item = "Produto" };

        var sale = new Sale();
        sale.Id = id;
        sale.Seller = seller;
        sale.Products = new List<Product>() { product };
        sale.Created = DateTime.Now;
        sale.Status = EnumStatus.Aguardando;

        return sale;
    }
}