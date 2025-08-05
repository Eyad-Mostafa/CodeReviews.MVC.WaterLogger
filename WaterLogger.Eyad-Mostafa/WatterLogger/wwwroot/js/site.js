function calculate() {
    var tbl = document.getElementById("records");
    var resultArea = document.getElementById("result");

    var total = 0;

    for (var i = 1; i < tbl.rows.length; i++) {
        total += parseFloat(tbl.rows[i].cells[2].innerHTML);
    }

    resultArea.innerHTML = `Total: ${total}`;
}
