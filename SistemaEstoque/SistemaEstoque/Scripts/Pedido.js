
function Salvarpedido() {
    debugger;
    //Data
    var data = $("#Data").val();

    //Cliente
    var cliente = $("#cliente").val();

    //Valor
    var valor = $("#Valor").val();

    var token = $('input [name="__RequestverificationToken"]').val();

    var tokenadr = $('form[faction="/Pedido/Create"] input[name="__RequestverificationToken"]').val();

    var headers = {};
    var headersadr = {};
    headers['__RequestverificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    //Gravar
    var url = "/Pedido/Create";

    $.ajax({
        url: url
        , type: "POST"
        , datatype: "json"
        , headers: headersadr
        , data: { Id: 0, Data: data, Cliente: cliente, Valor: valor, __RequestVerificationToken: token }
        , success: function (data) {
            if (data.Resultado > 0 ){
                debugger;
                ListarItens(data.Resultado);
            }
        }
    });
}

function ListarItens(idPedido) {
    debugger;
    var url = "/Itens/ListarItens";

    $.ajax({
        url: url
        , type: "GET"
        , data: { id: id }
        , datatype: "JSON"
        , success: function (data) {
            var divItens = $("#divItens");
            divItens.empty();
            divItens.show();
            divItens.html(data);
        }
    });
}

function SalvarItens() {
   
   
   
    var quantidade = $("#Quantidade").val();
    var produto = $("#NomeProduto").val();
    var marca = $("#Marca").val();

    var url = "/Itens/SalvarItens";
    debugger;
    $.ajax({
        url: url
        , type: "GET"
        , data: { Quantidade: quantidade, Produto: produto, Marca: marca }
        , datatype: "html"
        , success: function (data) {
            if (data.Resultado > 0) {
                debugger;
                ListarItens(idPedido);
            }
        }
    }
    );
}
