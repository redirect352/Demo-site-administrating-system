var f = document.getElementById('checkAll');
f.onchange = function (e) {
    var el = e.target || e.srcElement;
    var qwe = el.form.getElementsByClassName('custom-control-input');
    for (var i = 0; i < qwe.length; i++) {
        if (el.checked) {
            qwe[i].checked = true;
        } else {
            qwe[i].checked = false;
        }
    }
}

function ConvertUTCDateToLocal(date, id) {
    var localDate = new Date(date);

    var p = document.getElementById(id);
    p.textContent = localDate.toString();
    
}
