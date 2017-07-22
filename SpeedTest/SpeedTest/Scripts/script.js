function TestSite() {
    var url = document.getElementById('siteAddress').value;

    $.get('/home/Measure', { siteUrl: url},
    function (returnedData) {
        console.log(returnedData);
    });
}

function AddTable() {
    var obj = [
        {
            url: "http://dsadas.dsd",
            time: 13123,
        },
        {
            url: "http://dsadadsaqs.dsd",
            time: 132123,
        },
        {
            url: "http://abc.ua",
            time: 678,
        }
    ]
    var tbl = $("<table/>").attr("id", "mytable");
    $("#div1").append(tbl);
    for (var i = 0; i < obj.length; i++) {
        var tr = "<tr>";
        var td1 = "<td>" + obj[i]["url"] + "</td>";
        var td2 = "<td>" + obj[i]["time"] + "</td>";
        var tr2 = "</tr>";

        $("#mytable").append(tr + td1 + td2 + tr2);

    }
}

function AddToTable(Urls) {
    var obj = urls;

    for (var i = 0; i < obj.length; i++) {
        tr = "<tr><td>"+obj[i].url +"</td><td>"+obj[i].time+"</td></tr>"
        $('#resultsTable tbody').append(tr);
    }
}