function GenericAjaxfunc(URL, Type, Dataa, SuccessCallback, FailureCallback, token) {
 
    $.ajax({
        type: Type
        , url: URL
        , headers: {Authorization: 'Bearer ' + token}
        , data: Dataa
        , success: SuccessCallback
        , error: FailureCallback
    })
}
