var ajaxHistory = {
    onRestore : null,
    Restore: function () {                
        var history = angular.element(document.querySelector("#ajaxHistory")).val();                              
        if (history.length != 0 && this.onRestore != null) 
            this.onRestore(JSON.parse(history));                
    },
    Save: function (data) {               
        angular.element(document.querySelector("#ajaxHistory")).val(JSON.stringify(data));
    }
};