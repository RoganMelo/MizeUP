/* Vari�veis que recebem os seletores. */
var body = $('body');
var navExpander = $('#nav-expander');
var menu = $(".menu");
var divFilterMenu = $("div[id$=filter-menu]");
var mainMenu = $(".main-menu");
var popover = $('#popover');
var divPopover = $('#div-popover');
var login = $(".login");
var btnUser = $('#btn-user');
var divIconLoad = $("div[id$=icon-load]");
var divFilterIconLoad = $("div[id$=filter-load]");
/* Fim de vari�veis que recebem os seletores. */

/* Vari�veis de tempo dos efeitos. */
var timeMenu = 600;
var dropdownMenu = 300;
var timeIconLoad = 1500;
/* Fim de vari�veis de tempo dos efeitos. */

/* Vari�veis booleanas. */
var isMenuHide = true;
var isPopoverHide = true;
/* Fim de vari�veis booleanas. */

/* Script executado quando a p�gina � carregada. */
$(document).ready(function () {
    divFilterMenu.hide(); //Esconde o filtro do menu.
    loadIcon(false); // Chama a fun��o que ativa(true) ou desativa(false) as divs de load.
});
/* Fim do script excutado quando a p�gina � carregada. */

/* Script do menu lateral retr�til. */
navExpander.on('click', function (e) {
    e.preventDefault();
    if (isMenuHide) {
        body.toggleClass('nav-expanded');
        menu.css("color", "#3C599F");
        menu.css("background", "white");
        divFilterMenu.css("left", "17.15em");
        divFilterMenu.fadeIn(timeMenu);
        divFilterMenu.height($(document).height());
    } else {
        body.toggleClass('nav-expanded');
        menu.css("color", "white");
        menu.css("background", "#3C599F");
        divFilterMenu.css("left", "0");
        divFilterMenu.fadeOut(timeMenu);
        divFilterMenu.height($(document).height());
    }
    isMenuHide = !isMenuHide;
});

/* Script do dropdown dentro do menu. */
mainMenu.navgoco({
    accordion: false,
    openClass: 'open',
    save: true,
    cookie: {
        name: 'navgoco',
        expires: false,
        path: '/'
    },
    slide: {
        duration: dropdownMenu,
        easing: 'swing'
    }
});
/* Fim do script do dropdown dentro do menu. */
/* Fim do script do menu lateral retr�til. */

/* Script do popover de login. */
popover.popover({
    trigger: 'manual',
    html: true,
    placement: 'bottom',
    content: divPopover.html()
}).click(function (e) {
    e.preventDefault();
    if (isPopoverHide) {
        $(this).popover('show');
        login.css("color", "#3C599F");
        login.css("background", "white");

        if (!isMenuHide) {
            body.toggleClass('nav-expanded');
            menu.css("color", "white");
            menu.css("background", "#3C599F");
            divFilterMenu.css("left", "0");
            divFilterMenu.fadeOut(timeMenu);
            divFilterMenu.height($(document).height());
            isMenuHide = !isMenuHide;
        }
    } else {
        $(this).popover('hide');
        login.css("color", "white");
        login.css("background", "#3C599F");
    }
    isPopoverHide = !isPopoverHide;
});
/* Fim do script do popover de login. */

/* Script do bot�o de usu�rio. */
btnUser.click(function (e) {
    e.preventDefault();
    if (isPopoverHide) {
        $(this).popover('show');
        login.css("color", "#3C599F");
        login.css("background", "white");
    } else {
        $(this).popover('hide');
        login.css("color", "white");
        login.css("background", "#3C599F");
    }
    isPopoverHide = !isPopoverHide;
});
/* Fim do script do bot�o de usu�rio. */

/* Classe geral do modal. */
var Modal = function (op) {
    this.modal = $("#modal");
    this.title = this.modal.find("#title");
    this.body = this.modal.find("#body");
    this.footer = this.modal.find("#footer");
    this.textBody = this.modal.find("#text-body");
    this.btnYes = this.modal.find("#btn-yes");
    this.btnNot = this.modal.find("#btn-not");

    this.title.text(op.title);
    this.textBody.text(op.textBody);
    this.btnNot.text(op.textBtnNot);
    this.btnYes.text(op.textBtnYes);

    this.setContentBody = function (content) {
        this.body.html(content);
    };

    this.setContentFooter = function (content) {
        this.footer.html(content);
    };

    this.setClickBtnYes = function (func) {
        this.btnYes.click(func);
    };

    this.setClickBtnNot = function (func) {
        this.btnNot.click(func);
    };

    this.removeBody = function () {
        this.body.remove();
    }

    this.removeFooter = function () {
        this.footer.remove();
    };

    this.show = function () {
        this.modal.modal('show');
    };

    this.hide = function () {
        this.modal.modal('hide');
    };

    this.setFunctionShow = function (func) {
        this.modal.on("show.bs.modal", func);
    };

    this.setFunctionHide = function (func) {
        this.modal.on("hide.bs.modal", func);
    };
}
/* Fim da classe geral do modal. */

/* Fun��o que ativa(true) ou desativa(false) as divs de load. */
function loadIcon(status) {
    if (status == true) {
        divIconLoad.fadeIn(timeIconLoad);
        divFilterIconLoad.fadeIn(timeIconLoad);
        divFilterIconLoad.height($(document).height());
    }
    else {
        divIconLoad.fadeOut(timeIconLoad);
        divFilterIconLoad.fadeOut(timeIconLoad);
        divFilterIconLoad.height($(document).height());
    }

}
/* Fim da fun��o que ativa(true) ou desativa(false) as divs de load. */