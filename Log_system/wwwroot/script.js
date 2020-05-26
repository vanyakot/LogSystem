$(function() {
    $('#date').daterangepicker({
        timePicker: true,
        startDate: moment().startOf('day'),
        endDate: moment().startOf('day').add(24, 'hour'),
        locale: {
            format: 'MM/DD/YY HH:mm'
        }
    });
});

async function getData(){
  var data = [];
  var hotelId = document.getElementById('HotelId').value;
  var dates = document.getElementById('date').value;
  var typeOfData = document.getElementById('TypeOfData').value;
  
  var StartDate = moment(new Date(dates.split(' - ')[0])).format('YYYY-MM-DDTHH:mm:ss');
  var EndDate = moment(new Date(dates.split(' - ')[1])).format('YYYY-MM-DDTHH:mm:ss');

  if (typeOfData == 'Error'){
      getErrors(hotelId, StartDate, EndDate);
  } else if (typeOfData == 'Warning'){
      getWarnings(hotelId, StartDate, EndDate);

  }

  getDataChart(StartDate, EndDate);
}

async function getErrors(hotelId, StartDate, EndDate){
    var data = null;
    if (hotelId != '' && !isNaN(hotelId)){
        var response = await fetch("/get-log-data/errors/" + hotelId + "/" + StartDate + "/" + EndDate);  
    } else {
        var response = await fetch("/get-log-data/errors/" + StartDate + "/" + EndDate);
    }
    
    if (response.ok) {
        errors = await response.json();
    } else {
       alert("HTTP Error" + response.status);
    }

    renderErrorTable(errors);
}

async function getWarnings(hotelId, StartDate, EndDate){
    var warnings = null;
    if (hotelId != '' && !isNaN(hotelId)){
        var response = await fetch("/get-log-data/warnings/" + hotelId + "/" + StartDate + "/" + EndDate);  
    } else {
        var response = await fetch("/get-log-data/warnings/" + StartDate + "/" + EndDate);
    }

    if (response.ok) {
        warnings = await response.json();
    } else {
       alert("HTTP Error" + response.status);
    }

    renderWarningTable(warnings);
}



function renderWarningTable(warnings){
    var tableBody = document.getElementById('tableBody');
    tableBody.innerHTML = '';
    warnings.forEach(warningInfo => {
        warningInfo.creationDateTime = moment(errorInfo.creationDateTime).format('MMMM Do YYYY, HH:mm:ss');
        var tr = document.createElement('tr');
        appendTd(errorInfo.hotelId, tr);
        appendTd(errorInfo.error, tr);
        appendTd(errorInfo.creationDateTime, tr);
        tableBody.appendChild(tr);
    });  
}



function renderErrorTable(errors) {
    var tableBody = document.getElementById('tableBody');
    var counter = 0;
    tableBody.innerHTML = '';
    errors.forEach(errorInfo => {
        errorInfo.creationDateTime = moment(errorInfo.creationDateTime).format('MMMM Do YYYY, HH:mm:ss');
        var tr = document.createElement('tr');

        if (errorInfo.additionalInfo != ''){         
            tr.className = "clickable collapse-row collapsed table-active";
            tr.dataset.target = "#accordion" + counter;
            tr.dataset.toggle = "collapse";
            appendTd(errorInfo.hotelId, tr);
            appendTd(errorInfo.error, tr);
            appendTd(errorInfo.creationDateTime, tr);
            tableBody.appendChild(tr);
            var hidetr = document.createElement('tr');
            appendHideTd(errorInfo.additionalInfo, hidetr, counter);
            tableBody.appendChild(hidetr);
            counter ++;
        } else {
            appendTd(errorInfo.hotelId, tr);
            appendTd(errorInfo.error, tr);
            appendTd(errorInfo.creationDateTime, tr);
            tableBody.appendChild(tr);
        }
    });

}

function appendTd(data, tr) {
    var td = document.createElement('td');
    td.appendChild(document.createTextNode(data || ""));
    tr.appendChild(td);
}

function appendHideTd(data, tr, counter) {
    var td = document.createElement('td');
    td.colSpan = "3";
    var div = document.createElement('div');
    div.className = "collapse";
    div.id = "accordion" + counter;
    div.appendChild(document.createTextNode(data || ""));
    td.appendChild(div);
    tr.appendChild(td);
}

async function getDataChart(StartDate, EndDate){
    var data = null;
    var responseError = await fetch("/get-log-data/errors/charts/" + StartDate + "/" + EndDate);

    if (responseError.ok) {
        errorData = await responseError.json();
    } else {
        alert("HTTP Error" + responseError.status);
    }
    var responseWarning = await fetch("/get-log-data/warnings/charts/" + StartDate + "/" + EndDate);

    if (responseWarning.ok) {
        warningData = await responseWarning.json();
    } else {
        alert("HTTP Error" + responseWarning.status);
    }

    chart = drawChart();  
    addData(chart, errorData, warningData);
}


function drawChart(){
    return new Chart(document.getElementById("line-chart"), {
        type: 'line',
        data: {
            labels: [],
            datasets: [{ 
                data: [],
                label: "Error",
                borderColor: "#3e95cd",
                fill: false
            }, { 
                data: [],
                label: "Warning",
                borderColor: "#8e5ea2",
                fill: false
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Количество Error и Warning'
            }
        }
    });
}

function addData(chart, dataError, dataWarning) {
    for (key in dataError){
        chart.data.labels.push(key);
        chart.data.datasets[0].data.push(dataError[key]);
    }

    for (key in dataWarning){
        chart.data.datasets[1].data.push(dataWarning[key]);
    }
    chart.update();
}



