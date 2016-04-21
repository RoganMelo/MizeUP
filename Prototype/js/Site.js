var timeEffect = 800;
$(document).ready(function () {
    // Popovers
    var verificaPopover = true;
    // Popover desktop.
    $('#popover').popover({
        trigger: 'manual',
        html: true,
        placement: 'bottom',
        content: $('#divPopover').html() // Adiciona o conte�do da div oculta para dentro do popover.
    }).click(function (e) {
        e.preventDefault();
        // Exibe ou oculta o popover.
        if (verificaPopover) {
            $(this).popover('show');
        } else {
            $(this).popover('hide');
        }
        verificaPopover = !verificaPopover;
    });
    // Fim popover desktop.

    //Popover mobile.
    $('#popoverMobile').popover({
        trigger: 'manual',
        html: true,
        placement: 'bottom',
        content: $('#divPopoverMobile').html() // Adiciona o conte�do da div oculta para dentro do popover.
    }).click(function (e) {
        e.preventDefault();
        // Exibe ou oculta o popover.
        if (verificaPopover) {
            $(this).popover('show');
        } else {
            $(this).popover('hide');
        }
        verificaPopover = !verificaPopover;
    });
    //Fim popover mobile.

    //Popover chat.
    var verificaPopoverChat = true;
    $('#popoverChat').popover({
        trigger: 'manual',
        html: true,
        placement: 'top',
        content: $('#divPopoverChat').html() // Adiciona o conte�do da div oculta para dentro do popover.
    }).click(function (e) {
        e.preventDefault();
        // Exibe ou oculta o popover.
        if (verificaPopover) {
            $(this).popover('show');
        } else {
            $(this).popover('hide');
        }
        verificaPopoverChat = !verificaPopoverChat;
    });
    //Fim popover chat.

    //Oculta divLogin na versao mobile
    $('#btnMenu').click(function () {
        $('#divLogin').hide();
    });
    //Fim oculta divLogin na versao mobile

    var verificaTabela = true;
    // Esconde tabela.
    $('#ocultaTabela').click(function (e) {
        e.preventDefault();
        if (verificaTabela) {
            $('table').show(timeEffect);
            $(this).html("<i class='glyphicon glyphicon-minus'></i> Ocultar Tabela");
        } else {
            $('table').hide(timeEffect);
            $(this).html("<i class='glyphicon glyphicon-plus'></i> Mostrar Tabela");
        }
        verificaTabela = !verificaTabela;
    });
    // Fim esconde tabela.
    
    
    //Tela de Incluir Disciplinas
     var html = '<div class="novo-horario"><div class="row">' +
							'<div class="col-sm-4"><label class="visible-xs-inline visible-sm-inline">Hora Início:</label><input type="text" class="form-control" placeholder="00:00" maxlength="5"></div>' +
							'<div class="col-sm-4"><label class="visible-xs-inline visible-sm-inline">Hora Término:</label><input type="text" class="form-control" placeholder="00:00" maxlength="5"></div>' +
							'<div class="col-sm-4">' +
                                '<label class="visible-xs-inline visible-sm-inline">Dia:</label>' +
								'<select class="form-control">' +
									'<option>Selecione o dia</option>' +
									'<option>Domingo</option>' +
									'<option>Segunda</option>' +
									'<option>Terça</option>' +
									'<option>Quarta</option>' +
									'<option>Quinta</option>' +
									'<option>Sexta</option>' +
									'<option>Sabado</option>' +	
								'</select>' +	
							'</div>' +
						'</div><br></div>';
						
        var btnAddNovoHorario = $("#btnAddNovoHorario");
        var divAddNovoHorario = $("#divAddNovoHorario");
        var btnRemoveNovaHorario = $("#btnRemoveNovoHorario");
        
        btnRemoveNovaHorario.click(function(){
            var novosHorarios = $(".novo-horario");
            if(!novosHorarios.length)
                alert("Não há nenhum horario novo para ser removido");
            else
                novosHorarios.eq(novosHorarios.length - 1).hide(timeEffect, function(){ 
                    $(this).remove(); 
                });
        });
        
        btnAddNovoHorario.click(function(){
            divAddNovoHorario.append(html);
            var novosHorarios = divAddNovoHorario.find(".novo-horario");
            novosHorarios.eq(novosHorarios.length - 1).hide().show(timeEffect);
        });

        //Adicionando novo membro nas atividades
        var modal = '<div class="novo-membro" style="margin: 5px 5px;"><div class="row">' +
                        '<div class="col-dm-12">'+
                            '<label class="visible-xs-inline visible-sm-inline">Membro:</label><input type="text" class="form-control" placeholder="Membro" maxlength="50">'+
                        '</div>' +
                    '</div>';
                        
        var btnAddMembro = $("#btnAddMembro");
        var divAddNovoMembro = $("#divAddNovoMembro");
        var btnRemoveMembro = $("#btnRemoveMembro");
        
        btnRemoveMembro.click(function(){
            var novosMembros = $(".novo-membro");
            if(!novosMembros.length)
                alert("Não há nenhum membro novo para ser removido");
            else
                novosMembros.eq(novosMembros.length - 1).hide(timeEffect, function(){ 
                    $(this).remove(); 
                });
        });
        
        btnAddMembro.click(function(){
            divAddNovoMembro.append(modal);
            var novosMembros = divAddNovoMembro.find(".novo-membro");
            novosMembros.eq(novosMembros.length - 1).hide().show(timeEffect);
        });
        //fim do modal de novos membros


    //Fim Tela de Incluir Disciplinas

        $('a[href^="#"]').on('click', function (e) {
            e.preventDefault();

            var target = this.hash;
            var $target = $(target);

            $('html, body').stop().animate({
                'scrollTop': $target.offset().top
            }, 900, 'swing', function () {
                window.location.hash = target;
            });
        });

        AcionarIconeLoad(false);
});

//Tela de Mostrar Disiciplinas
var showModalDelete = function (nameDisciplina){
    var modal = $("#modalDelete");
    var snap = modal.find("#nameDisciplina");
    
    snap.text(nameDisciplina);
    modal.modal();
    
};
    
var showModalDetails = function(id){
    //Pegar o id e trazer os dados via Ajax.
    var modal = $("#modalDetails");  
    modal.modal();
};
//Fim Tela de Mostrar Disiciplinas

var showModalGrades = function (nameDisciplina){
    var modal = $("#modalGrades");
    var snap = modal.find("#nameDisciplina");

    snap.text(nameDisciplina);
    modal.modal();
};

var showModalActivity = function (nameDisciplina){
    var modal = $("#modalActivity");
    var snap = modal.find("#nameDisciplina");

    snap.text(nameDisciplina);
    modal.modal();
};

function AcionarIconeLoad(valorEstado) {
    var tempo = 2500;
    if (valorEstado == true) {
        $("div[id$=dvProgress]").fadeIn(tempo);
        $("div[id$=filtroEscuro]").fadeIn(tempo);
        $("div[id$=filtroEscuro]").height($(document).height());
    }
    else {
        $("div[id$=dvProgress]").fadeOut(tempo);
        $("div[id$=filtroEscuro]").fadeOut(tempo);
        $("div[id$=filtroEscuro]").height($(document).height());
    }
}