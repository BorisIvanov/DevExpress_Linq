function FilterCB_KeyDown(s, e) {
	if (e.htmlEvent.keyCode == 13) {
		var obj = document.getElementById(s.name);

		var fieldName = obj.attributes["data-field-name"].value;
		var clientName = obj.attributes["data-client-name"].value;
		var grid = eval(obj.attributes["data-grid-view-name"].value);
		var filterValue = document.getElementById(clientName+"_I").value;
		grid.AutoFilterByColumn(fieldName, filterValue);
	}
};
function FilterCB_BeginCallback(s, e) {
	var fieldName = document.getElementById(s.name).attributes["data-field-name"].value;
	e.customArgs['fieldName'] = fieldName;
};