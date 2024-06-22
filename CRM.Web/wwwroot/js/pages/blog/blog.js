$(function () {
    "use strict";  
    initSparkline();
    initDonutChart();
    getMorris('line', 'line_chart');
});


function getMorris(type, element) {
    
    if (type === 'line') {
        Morris.Line({
            element: element,
            data: [
                {
                    period: '2012',
                   طراحی_سایت: 458,
                    عکاسی: 450,
                    تکنولوژی: 501,
                    سبک_زندگی: 410,
                    ورزش: 400
                },{
                    period: '2013',
                   طراحی_سایت: 340,
                    عکاسی: 201,
                    تکنولوژی: 150,
                    سبک_زندگی: 214,
                    ورزش: 250
                },{
                    period: '2014',
                   طراحی_سایت: 458,
                    عکاسی: 450,
                    تکنولوژی: 501,
                    سبک_زندگی: 410,
                    ورزش: 400
                }, {
                    period: '2015',
                   طراحی_سایت: 265,
                    عکاسی: 521,
                    تکنولوژی: 265,
                    سبک_زندگی: 220,
                    ورزش: 436
                }, {
                    period: '2016',
                   طراحی_سایت: 200,
                    عکاسی: 215,
                    تکنولوژی: 280,
                    سبک_زندگی: 300,
                    ورزش: 249
                },
                {
                    period: '2017',
                   طراحی_سایت: 865,
                    عکاسی: 450,
                    تکنولوژی: 501,
                    سبک_زندگی: 410,
                    ورزش: 400
                }
            ],
            xkey: 'period',
            ykeys: ['طراحی_سایت', 'عکاسی', 'تکنولوژی', 'سبک_زندگی', 'ورزش'],
            labels: ['طراحی_سایت', 'عکاسی', 'تکنولوژی', 'سبک_زندگی', 'ورزش'],
            pointSize: 2,
            fillOpacity: 0,
            pointStrokeColors: ['#78b83e', '#ec3b57', '#ffc107', '#ba3bd0', '#cddc39'],
            behaveLikeLine: true,
            gridLineColor: '#e0e0e0',
            lineWidth: 1,
            hideHover: 'auto',
            lineColors: ['#78b83e', '#ec3b57', '#ffc107', '#ba3bd0', '#cddc39'],
            resize: true
        });
    }
}
//===============================================================================
function initSparkline() {
    $(".sparkline").each(function () {
        var $this = $(this);
        $this.sparkline('html', $this.data());
    });
}
//===============================================================================
function initDonutChart() {
    Morris.Donut({
        element: 'donut_chart',
        data: [{
            label: 'Chrome',
            value: 30
        }, {
            label: 'Firefox',
            value: 25
        }, {
            label: 'Safari',
            value: 20
        }, {
            label: 'Opera',
            value: 10        
        }, {
            label: 'IE',
            value: 10
        },{
            label: 'سایر',
            value: 5
        }],
        colors: ['#7cd2dc', '#a989bf', '#afc966', '#f99d4a', '#f28c85', '#768c95 '],
        formatter: function (y) {
            return y + '%'
        }
    });
}
//===============================================================================
var data = [], totalPoints = 110;
function getRandomData() {
    if (data.length > 0) data = data.slice(1);

    while (data.length < totalPoints) {
        var prev = data.length > 0 ? data[data.length - 1] : 50, y = prev + Math.random() * 10 - 5;
        if (y < 0) { y = 0; } else if (y > 100) { y = 100; }

        data.push(y);
    }

    var res = [];
    for (var i = 0; i < data.length; ++i) {
        res.push([i, data[i]]);
    }

    return res;
}

//===============================================================================
$(window).scroll(function() {
    $('.card .sparkline').each(function(){
    var imagePos = $(this).offset().top;

    var topOfWindow = $(window).scrollTop();
        if (imagePos < topOfWindow+400) {
            $(this).addClass("pullUp");
        }
    });
});
//===============================================================================
//===============================================================================
$(function () {
    $('#world-map-markers').vectorMap({
        map: 'world_mill_en',
        normalizeFunction: 'polynomial',
        hoverOpacity: 0.7,
        hoverColor: false,
        backgroundColor: 'transparent',
        showTooltip: true,        

        regionStyle: {
            initial: {
                fill: 'rgba(210, 214, 222, 1)',
                "fill-opacity": 1,
                stroke: 'none',
                "stroke-width": 0,
                "stroke-opacity": 1
            },
            hover: {
                fill: 'rgba(255, 193, 7, 2)',
                cursor: 'pointer'
            },
            selected: {
                fill: 'yellow'
            },
            selectedHover: {}
        },
        markerStyle: {
            initial: {
                fill: '#fff',
                stroke: '#FFC107 '
            }
        },
        markers: [
            { latLng: [37.09,-95.71], name: 'America' },
            { latLng: [51.16,10.45], name: 'Germany' },
            { latLng: [-25.27, 133.77], name: 'Australia' },
            { latLng: [56.13,-106.34], name: 'Canada' },
            { latLng: [20.59,78.96], name: 'India' },
            { latLng: [55.37,-3.43], name: 'United Kingdom' },
        ]
    });
});

$('#usa').vectorMap({
    map : 'us_aea_en',
    backgroundColor : 'transparent',
    regionStyle : {
        initial : {
            fill : '#72c2ff'
        }
    }
}); 