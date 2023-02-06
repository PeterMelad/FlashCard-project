function GenericAxiosfunc(URL, Type, token, RedirectPage) {
    console.log(URL)
   // axios.defaults.headers.common['Authorization'] = `Bearer ${token}`

    //if (Type == 'get') {
    //    var respdata = '';
    //    axios.get(URL).then(resp => (respdata = resp.data));
    //    //  return respdata;
    //    return true;
    //}

    //if (Type == 'delete') {
        axios.delete(URL).then(response => {
            console.log(response);
            if (response.status == 200) {
                location.href = RedirectPage;
            }
            else {
                toastr.error('Invalid id')
            }
        });

    //}
}