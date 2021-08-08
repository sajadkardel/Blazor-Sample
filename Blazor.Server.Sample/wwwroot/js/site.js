window.ShowToastr = (type, message) => {
    if (type=== 'success') {
        toastr.success(message, 'success');
    }
    if (type === 'error') {
        toastr.error(message, 'error');
    }
}