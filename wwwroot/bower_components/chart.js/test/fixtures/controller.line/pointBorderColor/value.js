module.exports = {
	config: {
		type: 'line',
		data: {
			labels: [0, 1, 2, 3, 4, 5],
			datasets: [
				{
					// option in dataset
					data: [0, 5, 10, null, -10, -5],
					pointBorderColor: '#ff0000'
				},
				{
					// option in element (fallback)
					data: [4, -5, -10, null, 10, 5],
				}
			]
		},
		options: {
			legend: false,
			title: false,
			elements: {
				line: {
					fill: false,
				},
				point: {
					borderColor: '#00ff00',
					radius: 10,
				}
			},
			scales: {
				xAxes: [{display: false}],
				yAxes: [{display: false}]
			}
		}
	},
	options: {
		canvas: {
			height: 256,
			width: 512
		}
	}
};
