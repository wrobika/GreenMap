﻿function createSearchModel() {
    var searchModel = {};
    searchModel.NazwaObiektu = $('#NazwaObiektu').val();
    searchModel.NrRbdh = $('#NrRbdh').val();
    searchModel.Lokalizacja = $('#Lokalizacja').val();
    searchModel.Status = $('#Status').val();
    searchModel.EurefX1 = $('#EurefX1').val();
    searchModel.EurefX2 = $('#EurefX2').val();
    searchModel.EurefY1 = $('#EurefY1').val();
    searchModel.EurefY2 = $('#EurefY2').val();
    searchModel.GlebokoscZwierciadla1 = $('#GlebokoscZwierciadla1').val();
    searchModel.GlebokoscZwierciadla2 = $('#GlebokoscZwierciadla2').val();
    searchModel.KlasaFiltracji = $('#KlasaFiltracji').val();
    searchModel.Filtracja1 = $('#Filtracja1').val();
    searchModel.Filtracja2 = $('#Filtracja2').val();
    searchModel.PowierzchniaZieleni1 = $('#PowierzchniaZieleni1').val();
    searchModel.PowierzchniaZieleni2 = $('#PowierzchniaZieleni2').val();
    return JSON.stringify(searchModel);
}

function emptySearchForm() {
    $('#NazwaObiektu').val("");
    $('#NrRbdh').val("");
    $('#Lokalizacja').val("");
    $('#Status').val("");
    $('#EurefX1').val("");
    $('#EurefX2').val("");
    $('#EurefY1').val("");
    $('#EurefY2').val("");
    $('#GlebokoscZwierciadla1').val("");
    $('#GlebokoscZwierciadla2').val("");
    $('#KlasaFiltracji').val("");
    $('#Filtracja1').val("");
    $('#Filtracja2').val("");
    $('#PowierzchniaZieleni1').val("");
    $('#PowierzchniaZieleni2').val("");
}

function search() {
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: 'api/Search',
        data: createSearchModel(),
        cache: false,
        success: function (data) { loadFeatures(data) },
        error: function (jqXHR) { console.log(jqXHR) }
    });
    $('#searchModal').modal('hide');
}

function searchRemove() {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'api/Odwiert',
        cache: false,
        success: function (data) { loadFeatures(data) },
        error: function (jqXHR) { console.log(jqXHR) }
    });
    emptySearchForm();
    $('#searchModal').modal('hide');
}

function loadFeatures(data) {
    map.getLayers().forEach(function (layer) {
        if (layer.get('name') === 'drilling') {
            var featuresArray = [];
            for (var id of Object.keys(data)) {
                var feature = wktReader.readFeature(data[id]);
                feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
                feature.set('color', layerProperties['drilling'].color);
                feature.setId(id);
                featuresArray.push(feature);
            }
            layer.getSource().getSource().clear();
            layer.getSource().getSource().addFeatures(featuresArray);
        }
    });
}

let searchSubmitButton = document.getElementById('searchSubmit');
searchSubmitButton.addEventListener("click", search);
let searchRemoveButton = document.getElementById('searchRemove');
searchRemoveButton.addEventListener("click", searchRemove);
