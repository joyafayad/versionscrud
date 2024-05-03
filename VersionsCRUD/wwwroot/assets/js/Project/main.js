
//$(document).ready(function () {
//    var tableData = [
//        ["Tiger Nixon", "System Architect", "Edinburgh", "5421", "2011/04/25", "$3,120", "<button>update</button>", "<button>delete</button>"],
//        ["Garrett Winters", "Director", "Edinburgh", "8422", "2011/07/25", "$5,300"]
//    ];
//    // Populate the table body with the data
//    var table = $('#example').DataTable({
//        data: tableData
//    });
//});

document.addEventListener("DOMContentLoaded", function () {
    var tableData = [
        ["Tiger Nixon", "System Architect", "Edinburgh", "5421", "2011/04/25", "$3,120", "<button>edit</button>", "<button>delete</button>"],
        ["Garrett Winters", "Director", "Edinburgh", "8422", "2011/07/25", "$5,300", "",""]
    ];
    // Populate the table body with the data
    var table = $('#example').DataTable({
        data: tableData
    });
});

//document.addEventListener("DOMContentLoaded", function () {
//        $this = $(this);
//        var dtRow = $this.closest('tr');
//        if (dtRow.length > 0) {
//            $('div.modal-body').empty();
//            $('div.modal-body').append('Row index: ' + dtRow[0].rowIndex + '<br/>');
//            $('div.modal-body').append('Number of columns: ' + dtRow[0].cells.length + '<br/>');
//            for (var i = 0; i < dtRow[0].cells.length; i++) {
//                $('div.modal-body').append('Cell (column, row) ' + dtRow[0].cells[i]._DT_CellIndex.column + ', ' + dtRow[0].cells[i]._DT_CellIndex.row + ' => innerHTML : ' + dtRow[0].cells[i].innerHTML + '<br/>');
//            }
//            $('#myModal').modal('show');
//        } else {
//            console.error('Parent table row not found.');
//        }
//    });

  



//$('.dt-delete').each(function () {
//	$(this).on('click', function (evt) {
//		$this = $(this);
//		var dtRow = $this.parents('tr');
//		if (confirm("Are you sure to delete this row?")) {
//			var table = $('#example').DataTable();
//			table.row(dtRow[0].rowIndex - 1).remove().draw(false);
//		}
//	});
//});
//$('#myModal').on('hidden.bs.modal', function (evt) {
//	$('.modal .modal-body').empty();
//});
//});