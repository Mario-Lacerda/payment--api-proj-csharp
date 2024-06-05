using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Payment_API.src.DTOs;
using Payment_API.src.Models;
using Payment_API.src.Models.Interfaces;

namespace Payment_API.src.Controllers
{
    [ApiController]
    [Route("api-docs/[controller]")]
    [Produces("application/json")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _service;
        private readonly IMapper _mapper;

        public SalesController(ISaleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Buscar os dados de uma venda registrada
        /// </summary>
        /// <param name="id"> Informar o id da venda para recuperar os dados</param>
        /// <returns> Os dados do vendedor, a data do registro, a lista dos itens vendidos e o status da venda</returns>
        /// <response code="200"> Success - Exibe os dados do vendedor, a data do registro, a lista dos itens vendidos e o status da venda</response>
        /// <response code="404"> Not Found - Se o id não for encontrado ou for inválido </response>
        [HttpGet("{id}", Name = "Buscar venda")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SaleDTO> GetById(int id)
        {
            var sale = _service.GetById(id);

            if (sale is null)
                return NotFound( new { msg = $"A venda com id {id} não foi encontrada."});
            
            var saleDTO = _mapper.Map<SaleDTO>(sale);

            return saleDTO;
        }

        /// <summary>
        /// Registrar os dados de uma venda
        /// </summary>
        /// <param name="newSaleDTO"> Informar os dados da venda. </param>
        /// <returns> Um novo registro de venda </returns>
        /// <remarks>
        /// Exemplo de request:  
        ///  
        ///     POST  
        ///     {  
        ///       "seller": {  
        ///         "id": 0,  
        ///          "name": "string",  
        ///          "cpf": "000.000.000-00",  
        ///          "email": "user@example.com",  
        ///          "telephone": "00-0000-0000"  
        ///     },  
        ///       "products": [  
        ///       {  
        ///         "id": 0,  
        ///         "item": "string"  
        ///       }  
        ///      ]  
        ///     }  
        ///
        /// Obs.:
        /// A Data/Hora e o Status da venda serão adicionados quando o registro for criado.  
        /// Caso o vendedor já esteja registrado basta adicionar o id do vendedor (seller).  
        /// </remarks>
        /// <response code="201"> Success - Retorna o registro criado </response>
        /// <response code="400"> Bad Request - Se alguma informação obrigatória estiver faltando ou se houver algum erro </response>
        /// <response code="500"> Internal Server Error - Erro inesperado </response>
        [HttpPost(Name = "Registrar venda")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] NewSaleDTO newSaleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sale = _mapper.Map<Sale>(newSaleDTO);
            var result = _service.Create(sale);

            var saleDTO = _mapper.Map<SaleDTO>(result);


            return CreatedAtAction(nameof(GetById), new { id = sale.Id}, saleDTO);
        }

        /// <summary>
        /// Atualizar o status de uma venda
        /// </summary>
        /// <param name="id"> Informar o id da venda para atualização </param>
        /// <param name="updateStatus"> Selecionar opção válida para atualizar o status</param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// Opções válidas para alteração do status:  
        ///  
        ///     De: "Aguardando"    Para: "Aprovado" ou "Cancelada"  
        ///     De: "Aprovado"      Para: "Enviado" ou "Cancelada"  
        ///     De: "Enviado"       Para: "Entregue"  
        ///  
        /// </remarks>
        /// <response code="204"> Success - Status atualizado.</response>
        /// <response code="404"> Not Found - Se o id não for encontrado ou for inválido </response>
        /// <response code="400"> Bad Request - Se o status selecionado for uma opção inválida </response>
        [HttpPatch("{id}", Name = "Atualizar Status da Venda")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateStatus(int id, EnumStatusUpdateDTO updateStatus)
        {
            var venda = _service.GetById(id);

            if (venda is null) 
                return NotFound( new { 
                    msg = $"A venda com id {id} não foi encontrada."});

            try
            {
                var status = _mapper.Map<EnumStatus>(updateStatus);
                _service.UpdateStatus(id, status);

            }
            catch (System.Exception)
            {
                return BadRequest( new { 
                    msg = $"Ocorreu erro ao enviar a solicitação de atualização do id {id}. Verifique Status selecionado."});
            }
            
            return NoContent();

        }
    }
}