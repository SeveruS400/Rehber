


    $(document).ready(function () {
        var table = $('#example').DataTable({
            responsive: true,
            language: {
                "decimal": "",
                "emptyTable": "Tabloda veri bulunamadı",
                "info": "_TOTAL_ kayıt içerisinden _START_ - _END_ arası gösteriliyor",
                "infoEmpty": "Kayıt yok",
                "infoFiltered": "(_MAX_ kayıt içerisinden filtrelendi)",
                "lengthMenu": "_MENU_ kayıt göster",
                "loadingRecords": "Yükleniyor...",
                "processing": "İşleniyor...",
                "search": "Ara:",
                "zeroRecords": "Eşleşen kayıt bulunamadı",
                "paginate": {
                    "first": "İlk",
                    "last": "Son",
                    "next": "Sonraki",
                    "previous": "Önceki"
                },
                "aria": {
                    "sortAscending": ": Artan şekilde sırala",
                    "sortDescending": ": Azalan şekilde sırala"
                }
            }
        });
    });

