/* Esconde tabela. */
var isTableHide = true;
var timeEffect = 800;

var btnHideTable = $("[data-btn-hide-table]").eq(0);
var table = $("[data-table]").eq(0);

var hide = "Hide", show = "Show", content = "Coloque a variavel language";
try {
    console.log(language);

    hide = language.hide;
    show = language.show;
    content = language.content;    
}catch(err){
    console.log("Default");
}


btnHideTable.click(function (e) {
    e.preventDefault();
    if (isTableHide) {
        table.fadeIn(timeEffect);
        btnHideTable.html("<i class='glyphicon glyphicon-chevron-down'></i> " + hide + " " + content);
    } else {
        table.fadeOut(timeEffect);
        btnHideTable.html("<i class='glyphicon glyphicon-chevron-right'></i> " + show + " " + content);
    }
    isTableHide = !isTableHide;
});
/* Fim esconde tabela. */