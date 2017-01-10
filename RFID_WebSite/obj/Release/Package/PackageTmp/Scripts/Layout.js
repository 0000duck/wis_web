var Layout = angular.module('Layout', []);
Layout.controller('Layout', function ($scope) {

    $scope.init = function () {
        $('.dragToolBox').draggable();
        $('.dragSelectMode').draggable();

        $(".dragToolBox").css('left', $(window).width() * 2 / 3).css('top', 70);
        $(".dragSelectMode").css('left', $(window).width() * 2 / 3).css('top', 70);
        $.contextMenu({
            // define which elements trigger this menu
            selector: ".layoutContent",
            //trigger: 'none',
            // define the elements of the menu
            items: {
                add: {
                    name: "Add", callback: function (key, opt) {
                        $scope.disableID = false;
                        $scope.targetHeight = '50';
                        $scope.targetWidth = '50';
                        $scope.targetTop = $scope.mouseTop;
                        $scope.targetLeft = $scope.mouseLeft;
                        $scope.targetZIndex = '1';
                        $scope.targetID = '';
                        $scope.targetCaption = '';

                        $scope.isDragEditWindowShow = true;
                        $scope.addMode = true;
                        $scope.$apply();
                        // $('.dragToolBox ').height(400);
                    }
                },
                edit: {
                    name: "Edit", callback: function (key, opt) {
                        $scope.disableID = true;
                        $scope.addMode = false;
                        $scope.isDragEditWindowShow = true;
                        $scope.$apply();
                        //$('.dragToolBox ').height(320);
                    }
                },
                remove: {
                    name: "Remove", callback: function (key, opt) {

                    }
                }
            }

        });



        $('.layoutContent').mousedown(function (e) {
            if (e.button == 2) {

                //$('.layoutContent').contextMenu();
                $scope.mouseTop = e.pageY + document.documentElement.scrollTop - $('.layoutContent').offset().top;
                $scope.mouseLeft = e.pageX + document.documentElement.scrollLeft - $('.layoutContent').offset().left;
                //$scope.targetCaption = e.pageY + '  ' + document.documentElement.scrollTop + '   ' + $('.layoutContent').offset().top + '  ' + parseInt($('.get-top-offset').css('height').replace('px', ''));
                if (e.target.id == '') {
                    e.target = $(e.target).parent()[0];
                }
                $scope.targetID = e.target.id;
                $scope.targetHeight = e.target.height;
                $scope.targetWidth = e.target.width;
                $scope.targetTop = e.target.top;
                $scope.targetLeft = e.target.left;
                $scope.targetCaption = $(e.target).text();
                $scope.targetZIndex = e.target.style.zIndex;
            }
        });
    }

    $scope.TypeModel = [{ type: 'SQUARE' },
                        { type: 'LABEL' },
                        { type: 'BLANK' },
                           { type: 'INFO_R' },
                           { type: 'INFO_L' },
                           { type: 'INFO_T' },
                           { type: 'INFO_B' }];

    $scope.addToLayout = function () {
        $scope.isDragEditWindowShow = false;

        printElementForEdit($scope.targetID, $scope.targetHeight, $scope.targetWidth, $scope.targetTop, $scope.targetLeft, $scope.targetCaption, $scope.targetZIndex, $scope.targetType);
        $('#' + $scope.targetID).resizable({ grid: [5, 5] }).draggable();
    }

    $scope.getLayout = function (name) {
        $http.post("@Url.Action("GetLayout", "Home")", { Name: $scope.LayoutName, listObject: JSON.stringify(listObject) }).success(function (response) {
            $.each(response, function (index, d) {
                // if already have elements, remove all
                $('#' + d.id).remove();
                printElementForEdit(d.id, d.height, d.width, d.top, d.left, d.caption, d.zIndex, d.type);

            });
        }).error(function (response) {
            alert(response);
        });


    }

    $scope.saveLayout = function () {

        var listObject = [];
        var divType = '';
        $('.layoutContent div').each(function () {

            if ($(this).hasClass('squareType')) {
                divType = 'SQUARE';
            } else if ($(this).hasClass('labelType')) {
                divType = 'LABEL';
            } else if ($(this).hasClass('blankType')) {
                divType = 'BLANK';
            } else if ($(this).hasClass('info_border')) {
                if ($(this).children("p")[0].hasClass('arrow_r_int')) {
                    divType = 'INFO_R';
                } else if ($(this).children("p")[0].hasClass('arrow_l_int')) {
                    divType = 'INFO_L';
                } else if ($(this).children("p")[0].hasClass('arrow_t_int')) {
                    divType = 'INFO_T';
                } else if ($(this).children("p")[0].hasClass('arrow_b_int')) {
                    divType = 'INFO_B';
                }
            }

            listObject.push({
                id: this.id,
                height: this.style.height,
                width: this.style.width,
                top: this.style.top,
                left: this.style.left,
                caption: $(this).children("p").text(),
                type: divType,
                zIndex: this.style.zIndex,
            });
        });
        $http.post("@Url.Action("SaveLayout", "Home")", { Name: $scope.LayoutName, listObject: JSON.stringify(listObject) }).success(function (response) {
            if (response.status == 'ERROR') {
                alert(response.detail);

            } else {
                //resetTimeout();
                alert(response.status);
                $scope.ShowSelectMode = false;
            }
        }).error(function (response) {
            alert(response);
        });



    };

    $scope.$watch('edit', function (newValue, oldValue) {
        if (newValue == false) {
            $('.layoutContent').contextMenu(false);
            $('.layoutContent div').remove();
            $scope.isDragEditWindowShow = false;

            $scope.getLayout($scope.LayoutName);
        } else {
            $('.layoutContent').contextMenu(true);

            $('.layoutContent div').resizable({ grid: [5, 5] }).draggable();
        }
    });
    $scope.init();

    function printElementForEdit(Id, height, width, top, left, caption, zIndex, type) {
        var div = document.createElement('div');

        div.style.height = height;
        div.style.width = width;
        div.style.top = top;
        div.style.left = left;
        div.style.zIndex = zIndex;
        div.id = Id;

        var p = document.createElement('p');
        $(p).addClass('objectCaption');
        $(p).text(caption);
        $(p).appendTo(div);

        switch (type) {
            case 'SQUARE':
                $(div).addClass('squareType');
                break;
            case 'LABEL':
                $(div).addClass('labelType');
                break;
            case 'BLANK':
                $(div).addClass('blankType');
                break;
            default:

                var span = document.createElement('span');
                var span1 = document.createElement('span');

                switch (type) {
                    case 'INFO_R':
                        $(span).addClass('arrow_r_int');
                        $(span1).addClass('arrow_r_out');
                        break;
                    case 'INFO_L':
                        $(span).addClass('arrow_l_int');
                        $(span1).addClass('arrow_l_out');
                        break;
                    case 'INFO_T':
                        $(span).addClass('arrow_t_int');
                        $(span1).addClass('arrow_t_out');
                        break;
                    case 'INFO_B':
                        $(span).addClass('arrow_b_int');
                        $(span1).addClass('arrow_b_out');
                        break;
                }
                $(span).appendTo(div);
                $(span1).appendTo(div);
                $(div).addClass('info_border');
                break;
        }
        $(div).appendTo('.layoutContent');

            

    }


})
