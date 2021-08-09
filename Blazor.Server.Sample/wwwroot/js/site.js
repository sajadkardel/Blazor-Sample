window.ShowToastr = (type, message) => {
    if (type=== 'success') {
        toastr.success(message, 'success');
    }
    if (type === 'error') {
        toastr.error(message, 'error');
    }
}

function showConfirmationModal() {
    $('#delete-confirm-modal').modal('show');
}

function hideConfirmationModal() {
    $('#delete-confirm-modal').modal('hide');
}