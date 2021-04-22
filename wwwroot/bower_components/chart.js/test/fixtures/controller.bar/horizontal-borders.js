module.exports = {
	threshold: 0.01,
	config: {
		type: 'horizontalBar',
		data: {
			labels: [0, 1, 2, 3, 4, 5],
			datasets: [
				{
					// option in dataset
					data: [0, 5, 10, null, -10, -5],
					borderWidth: 2
				},
				{
					// option in element (fallback)
					data: [0, 5, 10, null, -10, -5],
					borderSkipped: false,
				}
			]
		},
		options: {
			legend: false,
			title: false,
			elements: {
				rectangle: {
					backgroundColor: '#AAAAAA80',
					borderColor: '#80808080',
					borderWidth: {bottom: 6, left: 15, top: 6, right: 15}
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
