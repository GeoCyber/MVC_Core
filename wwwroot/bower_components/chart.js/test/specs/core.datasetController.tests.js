describe('Chart.DatasetController', function() {
	it('should listen for dataset data insertions or removals', function() {
		var data = [0, 1, 2, 3, 4, 5];
		var chart = acquireChart({
			type: 'line',
			data: {
				datasets: [{
					data: data
				}]
			}
		});

		var controller = chart.getDatasetMeta(0).controller;
		var methods = [
			'onDataPush',
			'onDataPop',
			'onDataShift',
			'onDataSplice',
			'onDataUnshift'
		];

		methods.forEach(function(method) {
			spyOn(controller, method);
		});

		data.push(6, 7, 8);
		data.push(9);
		data.pop();
		data.shift();
		data.shift();
		data.shift();
		data.splice(1, 4, 10, 11);
		data.unshift(12, 13, 14, 15);
		data.unshift(16, 17);

		[2, 1, 3, 1, 2].forEach(function(expected, index) {
			expect(controller[methods[index]].calls.count()).toBe(expected);
		});
	});

	describe('inextensible data', function() {
		it('should handle a frozen data object', function() {
			function createChart() {
				var data = Object.freeze([0, 1, 2, 3, 4, 5]);
				expect(Object.isExtensible(data)).toBeFalsy();

				var chart = acquireChart({
					type: 'line',
					data: {
						datasets: [{
							data: data
						}]
					}
				});

				var dataset = chart.data.datasets[0];
				dataset.data = Object.freeze([5, 4, 3, 2, 1, 0]);
				expect(Object.isExtensible(dataset.data)).toBeFalsy();
				chart.update();

				// Tests that the unlisten path also works for frozen objects
				chart.destroy();
			}

			expect(createChart).not.toThrow();
		});

		it('should handle a sealed data object', function() {
			function createChart() {
				var data = Object.seal([0, 1, 2, 3, 4, 5]);
				expect(Object.isExtensible(data)).toBeFalsy();

				var chart = acquireChart({
					type: 'line',
					data: {
						datasets: [{
							data: data
						}]
					}
				});

				var dataset = chart.data.datasets[0];
				dataset.data = Object.seal([5, 4, 3, 2, 1, 0]);
				expect(Object.isExtensible(dataset.data)).toBeFalsy();
				chart.update();

				// Tests that the unlisten path also works for frozen objects
				chart.destroy();
			}

			expect(createChart).not.toThrow();
		});

		it('should handle an unextendable data object', function() {
			function createChart() {
				var data = Object.preventExtensions([0, 1, 2, 3, 4, 5]);
				expect(Object.isExtensible(data)).toBeFalsy();

				var chart = acquireChart({
					type: 'line',
					data: {
						datasets: [{
							data: data
						}]
					}
				});

				var dataset = chart.data.datasets[0];
				dataset.data = Object.preventExtensions([5, 4, 3, 2, 1, 0]);
				expect(Object.isExtensible(dataset.data)).toBeFalsy();
				chart.update();

				// Tests that the unlisten path also works for frozen objects
				chart.destroy();
			}

			expect(createChart).not.toThrow();
		});
	});

	it('should synchronize metadata when data are inserted or removed', function() {
		var data = [0, 1, 2, 3, 4, 5];
		var chart = acquireChart({
			type: 'line',
			data: {
				datasets: [{
					data: data
				}]
			}
		});

		var meta = chart.getDatasetMeta(0);
		var first, second, last;

		first = meta.data[0];
		last = meta.data[5];
		data.push(6, 7, 8);
		data.push(9);
		expect(meta.data.length).toBe(10);
		expect(meta.data[0]).toBe(first);
		expect(meta.data[5]).toBe(last);

		last = meta.data[9];
		data.pop();
		expect(meta.data.length).toBe(9);
		expect(meta.data[0]).toBe(first);
		expect(meta.data.indexOf(last)).toBe(-1);

		last = meta.data[8];
		data.shift();
		data.shift();
		data.shift();
		expect(meta.data.length).toBe(6);
		expect(meta.data.indexOf(first)).toBe(-1);
		expect(meta.data[5]).toBe(last);

		first = meta.data[0];
		second = meta.data[1];
		last = meta.data[5];
		data.splice(1, 4, 10, 11);
		expect(meta.data.length).toBe(4);
		expect(meta.data[0]).toBe(first);
		expect(meta.data[3]).toBe(last);
		expect(meta.data.indexOf(second)).toBe(-1);

		data.unshift(12, 13, 14, 15);
		data.unshift(16, 17);
		expect(meta.data.length).toBe(10);
		expect(meta.data[6]).toBe(first);
		expect(meta.data[9]).toBe(last);
	});

	it('should re-synchronize metadata when the data object reference changes', function() {
		var data0 = [0, 1, 2, 3, 4, 5];
		var data1 = [6, 7, 8];
		var chart = acquireChart({
			type: 'line',
			data: {
				datasets: [{
					data: data0
				}]
			}
		});

		var meta = chart.getDatasetMeta(0);

		expect(meta.data.length).toBe(6);

		chart.data.datasets[0].data = data1;
		chart.update();

		expect(meta.data.length).toBe(3);

		data1.push(9, 10, 11);
		expect(meta.data.length).toBe(6);
	});

	it('should re-synchronize metadata when data are unusually altered', function() {
		var data = [0, 1, 2, 3, 4, 5];
		var chart = acquireChart({
			type: 'line',
			data: {
				datasets: [{
					data: data
				}]
			}
		});

		var meta = chart.getDatasetMeta(0);

		expect(meta.data.length).toBe(6);

		data.length = 2;
		chart.update();

		expect(meta.data.length).toBe(2);

		data.length = 42;
		chart.update();

		expect(meta.data.length).toBe(42);
	});

	it('should re-synchronize metadata when scaleID changes', function() {
		var chart = acquireChart({
			type: 'line',
			data: {
				datasets: [{
					data: [],
					xAxisID: 'firstXScaleID',
					yAxisID: 'firstYScaleID',
				}]
			},
			options: {
				scales: {
					xAxes: [{
						id: 'firstXScaleID'
					}, {
						id: 'secondXScaleID'
					}],
					yAxes: [{
						id: 'firstYScaleID'
					}, {
						id: 'secondYScaleID'
					}]
				}
			}
		});

		var meta = chart.getDatasetMeta(0);

		expect(meta.xAxisID).toBe('firstXScaleID');
		expect(meta.yAxisID).toBe('firstYScaleID');

		chart.data.datasets[0].xAxisID = 'secondXScaleID';
		chart.data.datasets[0].yAxisID = 'secondYScaleID';
		chart.update();

		expect(meta.xAxisID).toBe('secondXScaleID');
		expect(meta.yAxisID).toBe('secondYScaleID');
	});

	it('should cleanup attached properties when the reference changes or when the chart is destroyed', function() {
		var data0 = [0, 1, 2, 3, 4, 5];
		var data1 = [6, 7, 8];
		var chart = acquireChart({
			type: 'line',
			data: {
				datasets: [{
					data: data0
				}]
			}
		});

		var hooks = ['push', 'pop', 'shift', 'splice', 'unshift'];

		expect(data0._chartjs).toBeDefined();
		hooks.forEach(function(hook) {
			expect(data0[hook]).not.toBe(Array.prototype[hook]);
		});

		expect(data1._chartjs).not.toBeDefined();
		hooks.forEach(function(hook) {
			expect(data1[hook]).toBe(Array.prototype[hook]);
		});

		chart.data.datasets[0].data = data1;
		chart.update();

		expect(data0._chartjs).not.toBeDefined();
		hooks.forEach(function(hook) {
			expect(data0[hook]).toBe(Array.prototype[hook]);
		});

		expect(data1._chartjs).toBeDefined();
		hooks.forEach(function(hook) {
			expect(data1[hook]).not.toBe(Array.prototype[hook]);
		});

		chart.destroy();

		expect(data1._chartjs).not.toBeDefined();
		hooks.forEach(function(hook) {
			expect(data1[hook]).toBe(Array.prototype[hook]);
		});
	});
});
