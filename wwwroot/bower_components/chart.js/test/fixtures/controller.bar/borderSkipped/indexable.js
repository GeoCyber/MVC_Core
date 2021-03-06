module.exports = {
	config: {
		type: 'bar',
		data: {
			labels: [0, 1, 2, 3, 4, 5],
			datasets: [
				{
					// option in dataset
					data: [0, 5, 10, null, -10, -5],
					borderSkipped: [
						'top',
						'top',
						'right',
						'right',
						'bottom',
						'left'
					]
				},
				{
					// option in element (fallback)
					data: [0, 5, 10, null, -10, -5],
				}
			]
		},
		options: {
			legend: false,
			title: false,
			elements: {
				rectangle: {
					backgroundColor: 'transparent',
					borderColor: '#888',
					borderWidth: 8,
					borderSkipped: [
						'bottom',
						'bottom',
						'left',
						'left',
						'top',
						'right'
					]
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
