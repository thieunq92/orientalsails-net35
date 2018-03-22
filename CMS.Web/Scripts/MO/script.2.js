$(function () {
    if ($('#postBack').val() == '1') {
        $('#postBack').val(0);
    } else {
        document.location = document.location;
    }
    ;

    $('.booking-room').click(function (event) {
        if (!$(event.target).is('input')) {
            $(this).find('input[type="radio"]').prop('checked', true);
            $(this).find('input[type="radio"]').click();
        }
    });

    $('input[type="radio"]').prop('checked', false);
    $('input[type="radio"]').click(function (event) {
        if ($(this).is(':checked')) {
            // khi check mot radio button thi bien selected thanh current roi thay selected moi
            var roomid = $(this).parent().children('.field').val();

            $('.cruise-plan .selected').addClass('current');
            $('.cruise-plan .selected').removeClass('selected');
            if (roomid != '') {
                $('.cruise-plan .room' + roomid).addClass('selected');
            }
        }
    });

    function radioChecked() {
        
    }

    $('.cruise-plan .room').click(function () {
        // khi click vao phong thi check xem co dang chon bkroom nao khong
        if ($('input[type=radio]:checked').length > 0) {
            var p = $('input[type=radio]:checked').parent();

            var oldid = p.children('.field').val();
            // phong moi co du dieu kien de select hay khong
            var newid = $(this).html();

            if (p.find('.field').attr('readonly') == 'readonly') {
                return; // neu khoa thi khong cho phep thay doi
            }

            // neu phong lua chon nay la phong available
            if ($(this).hasClass('available')) {
                if (oldid != '') {
                    $('.cruise-plan .room' + oldid).removeClass('current');
                    $('.cruise-plan .room' + oldid).removeClass('selected');
                    $('.cruise-plan .room' + oldid).addClass('available'); //deselect
                }
                p.find('.field').val($(this).html()); // set field = id moi
                p.find('.room-name').html($(this).attr('name'));
                p.find('.type').html($(this).attr('type'));
                $(this).addClass('selected'); // select
                $(this).removeClass('available');
            }

            // neu tu click vao chinh minh
            else if ($(this).hasClass('selected')) {
                p.find('.field').val(''); // set field = null
                p.find('.room-name').html('');
                p.find('.type').html(p.find('.type').attr('rel'));
                $(this).removeClass('selected');
                $(this).removeClass('current');
                $(this).addClass('available');
            }

            // neu phong lua chon nay la phong current
            //            if ($(this).hasClass('current')) {
            //                if (oldid != '') {
            //                    $('.cruise-plan .room' + oldid).removeClass('current');
            //                    $('.cruise-plan .room' + oldid).removeClass('selected'); //deselect
            //                }
            //                p.children('.field').val($(this).html()); // set field = id moi
            //                $(this).addClass('selected'); // select
            //            }
        } else {
            alert('Please select the person you wish to arrange room first');
        }
    });
});