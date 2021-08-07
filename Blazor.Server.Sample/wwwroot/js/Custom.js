window.ShowToastr = (type, message) => {
    if (type=== 'success') {
        toastr.success(message, 'success text');
    }
    if (type === 'error') {
        toastr.error(message, 'error text');
    }
}