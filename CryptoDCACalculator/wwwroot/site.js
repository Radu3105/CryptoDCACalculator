window.initializeChart = (canvasId, labels, data, cryptoCurrencyName) => {
    var ctx = document.getElementById(canvasId).getContext('2d');
    if (window.myChart && typeof window.myChart.destroy === 'function') {
        window.myChart.destroy();
    }
    window.myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: `${cryptoCurrencyName} price over time`,
                data: data,
                borderColor: 'dodgerblue',
                borderWidth: 2,
                fill: false,
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: false
                }
            }
        }
    });
};
