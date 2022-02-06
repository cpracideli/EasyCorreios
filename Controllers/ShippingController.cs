using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;

namespace EasyCorreios.Controllers;

[ApiController]
[Route("api/[controller]")]

// Correios Doc: https://www.correios.com.br/atendimento/ferramentas/sistemas/arquivos/manual-de-implementacao-do-calculo-remoto-de-precos-e-prazos/view
// Example: https://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx?&nCdEmpresa=08082650&sDsSenha=564321&sCepOrigem=70002900&sCepDestino=04547000&nVlPeso=1&nCdFormato=1&nVlComprimento=20&nVlAltura=20&nVlLargura=20&sCdMaoPropria=n&nVlValorDeclarado=0&sCdAvisoRecebimento=n&nCdServico=04510&nVlDiametro=0&StrRetorno=xml&nIndicaCalculo=3
public class ShippingController : ControllerBase
{
    [HttpGet]
    async public Task<string> Get(
        string? nCdEmpresa,
        string? sDsSenha,
        string? nCdServico,
        string? sCepOrigem,
        string? sCepDestino,
        string? nVlPeso,
        string? nCdFormato,
        string? nVlComprimento,
        string? nVlAltura,
        string? nVlLargura,
        string? nVlDiametro,
        string? sCdMaoPropria,
        string? nVlValorDeclarado,
        string? sCdAvisoRecebimento
        )
    {

        string urlShippingCost = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx?";
        string queryCdEmpresa = nCdEmpresa.Length > 0 ? "&nCdEmpresa=" + nCdEmpresa : "";
        string querysDsSenha = sDsSenha.Length > 0 ? "&sDsSenha=" + sDsSenha : "";
        string querynCdServico = nCdServico.Length > 0 ? "&nCdServico=" + nCdServico : "";
        string querysCepOrigem = sCepOrigem.Length > 0 ? "&sCepOrigem=" + sCepOrigem : "";
        string querysCepDestino = sCepDestino.Length > 0 ? "&sCepDestino=" + sCepDestino : "";
        string querynVlPeso = nVlPeso.Length > 0 ? "&nVlPeso=" + nVlPeso : "";
        string querynCdFormato = nCdFormato.Length > 0 ? "&nCdFormato=" + nCdFormato : "";
        string querynVlComprimento = nVlComprimento.Length > 0 ? "&nVlComprimento=" + nVlComprimento : "";
        string querynVlAltura = nVlAltura.Length > 0 ? "&nVlAltura=" + nVlAltura : "";
        string querynVlLargura = nVlLargura.Length > 0 ? "&nVlLargura=" + nVlLargura : "";
        string querynVlDiametro = nVlDiametro.Length > 0 ? "&nVlDiametro=" + nVlDiametro : "";
        string querysCdMaoPropria = sCdMaoPropria.Length > 0 ? "&sCdMaoPropria=" + sCdMaoPropria : "";
        string querynVlValorDeclarado = nVlValorDeclarado.Length > 0 ? "&nVlValorDeclarado=" + nVlValorDeclarado : "";
        string querysCdAvisoRecebimento = sCdAvisoRecebimento.Length > 0 ? "&sCdAvisoRecebimento=" + sCdAvisoRecebimento : "";

        string fullUrl = urlShippingCost +
                            queryCdEmpresa +
                            querysDsSenha +
                            querynCdServico +
                            querysCepOrigem +
                            querysCepDestino +
                            querynVlPeso +
                            querynCdFormato +
                            querynVlComprimento +
                            querynVlAltura +
                            querynVlLargura +
                            querynVlDiametro +
                            querysCdMaoPropria +
                            querynVlValorDeclarado +
                            querysCdAvisoRecebimento +
                            "&StrRetorno=xml&nIndicaCalculo=3";

        var client = new HttpClient();
        var requestContent = new FormUrlEncodedContent(new[] {
            new KeyValuePair<string, string>("", ""),
        });

        HttpResponseMessage response = await client.GetAsync(fullUrl);

        string xml = await response.Content.ReadAsStringAsync();


        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);

        string json = JsonConvert.SerializeXmlNode(doc);

        return json;

    }
}
