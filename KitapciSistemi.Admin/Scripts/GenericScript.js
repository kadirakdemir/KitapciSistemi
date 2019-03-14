function KategoriEkle() {
    Kategori = new Object();
    Kategori.KategoriAdi = $("#kategoriAdi").val();
    Kategori.AltKategoriID = $("#AltKategoriID").val();
    Kategori.IsActive = $("#isActive").is(":checked");
    Kategori.Aciklama = $("#aciklama").val();

    $.ajax({
        url: "/Kategori/Insert",
        data: Kategori,
        type: "POST",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });

            }
            else {
                bootbox.alert(response.Message, function () {
                    // geridöndüğünde bir şey yapılması isteniyorsa buraya yazılır
                });
            }
        }
    })
}

$(document).on("click", "#KategoriDelete", function () {
    var gelenID = $(this).attr("data-id");
    var silTR = $(this).closest("tr");

    $.ajax({
        url: '/Kategori/Delete/' + gelenID,
        type: "POST",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                silTR.fadeOut(300, function () {
                    silTR.remove();
                })
            }
            else {
                $.notify(response.Message, "error");
            }
        }

    })
})

function KategoriDuzenle() {
    Kategori = new Object();
    Kategori.KategoriAdi = $("#kategoriAdi").val();
    Kategori.AltKategoriID = $("#AltKategoriID").val();
    Kategori.IsActive = $("#isActive").is(":checked");
    Kategori.Aciklama = $("#aciklama").val();
    Kategori.ID = $("#ID").val();

    $.ajax({
        url: "/Kategori/Update",
        data: Kategori,
        type: "POST",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });

            }
            else {
                bootbox.alert(response.Message, function () {
                    // geridöndüğünde bir şey yapılması isteniyorsa buraya yazılır
                });
            }
        }
    })
}

//dropdownlitfor castcading(iç içe bağımlı)

    $(function(){
        $("#KategoriID").change(function () {
            var id = $(this).val();           
            $.ajax({
                url: "/Urun/GetAltKategoriList",
                type:"POST",
                data: { 'AltID': id },
                dataType: 'json',
                success: function (result) {
                   alert(result)
                    var altkategori = $("#altKategoriID");
                    altkategori.empty();
                    $.each(result, function (i, item) {
                        //alert(item.KategoriAdi)
                        altkategori.append($("<option></option>").attr("value", item.ID)
                            .html(item.KategoriAdi));
                    });
                }
            });
        });
    });

    function KayitOl() {
        var User = new Object();
        User.KullaniciAdi = $("#username").val();
        User.Email = $("#email").val();
        User.Sifre = $("#password").val();
        $.ajax({
            url: "/Account/Create",
            data: User,
            type:"POST",
            datatype: 'json',
            success: function (data) {
                if (data.Success) {
                    bootbox.alert(data.Message, function () {
                        location.reload();
                    });
                } 
            }


        })
    }