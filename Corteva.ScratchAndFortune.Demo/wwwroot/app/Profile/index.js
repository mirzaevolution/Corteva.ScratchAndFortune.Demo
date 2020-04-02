$(document).ready(function() {
    dom.init();
});

var dom = {
    init: function() {
        $("#Province").select2({
            width: '100%',
            placeholder: "Pilih Provinsi"
        });
        $("#City").select2({
            width: '100%',
            placeholder: "Pilih Kota"
        });
        $("#Store").select2({
            width: '100%',
            placeholder: "Pilih Toko"
        });
        $("#Province").change(function() {
            $("#City").empty();
            $("#Store").empty();
            var selectedProvince = $("#Province").val();
            
            if (selectedProvince!=undefined && selectedProvince != null) {
                $.ajax({
                    url: "/Profile/GetCities",
                    method: "POST",
                    data: {
                        provinceId: selectedProvince
                    },
                    success: function(response) {
                        if (response && response.cities) {
                            
                            $.each(response.cities, function(index, value) {

                                $("#City").append("<option value='" + value.id + "'>" + value.name + "</option>");
                            });
                            $("#City").trigger("change");
                        }
                    }
                });
            }
        });
        $("#City").change(function() {
            var selectedCity = $("#City").val();
            $("#Store").empty();

            if (selectedCity != undefined && selectedCity != null) {
                $.ajax({
                    url: "/Profile/GetStores",
                    method: "POST",
                    data: {
                        cityId: selectedCity
                    },
                    success: function(response) {
                        if (response && response.stores) {
                            $.each(response.stores, function(index, value) {
                                $("#Store").append("<option value='" + value.name + "'>" + value.name + "</option>");
                            });
                        }
                    }
                });
            }
        });

        var currentProvince = $("#Province").val();
        $("#Province").val(currentProvince).trigger("change");

        this.addProductHandler();
        this.submitHandler();

    },
    addProductHandler: function() {
        $("#BtnAddProduct").click(function() {
            var htmlElements = "<div class='form-group product-container'>" +
                "<div class='d-inline-block m-2'>" +
                "<label class='control-label'>Produk</label>" +
                "<select class='form-control productName' style='width:165px'>" +
                "<option value='1'>Product 1</option>" +
                "<option value='2'>Product 2</option>" +
                "<option value='3'>Product 3</option>" +
                "<option value='4'>Product 4</option>" +
                "</select>" +
                "</div>" +
                "<div class='d-inline-block m-2'>" +
                "<label class='control-label'>Jumlah (Liter/KG)</label>" +
                "<input type='number' min='0' class='form-control quantity' style='max-width:115px' />" +
                "</div>" +
                "<div class='d-inline-block m-2'>" +
                "<button type='button' onclick='removeElement(this)' class='btn btn-danger'>Hapus</button>" +
                "</div></div>";
            $("#ProductBlock").append(htmlElements);
        });
    },
    submitHandler: function() {
        $("#BtnSubmit").click(function() {
            var productCounter = 0;
            var quantityCounter = 0;
            $(".productName").each(function(index, value) {
                $(value).attr("name", "SubmittedProducts[" + productCounter++ + "].ProductName");
            });
            $(".quantity").each(function(index, value) {
                $(value).attr("name", "SubmittedProducts[" + quantityCounter++ + "].Quantity");
            });
            $("#MainForm").submit();
        });
    }
};

function removeElement(btn) {
    $(btn).parents(".product-container")[0].remove();
}