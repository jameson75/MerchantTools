angular.module('orange', [])
    .directive('ngEnter', function () {
        return function (scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.ngEnter);
                    });
                    event.preventDefault();
                }
            });
        };
    })
    /*
    .directive('oibCenteredImg', function() {
        return {
            restri: "E",
            scope: {
                src: "@oibSrc",
                alt: "@oibAlt"
            },
            template: "<table width='100%' height='100%' align='center' valign='center'><tr><td><img /></td></tr></table>",
            transclude: true,
            replace: true,
            link: function (scope, element, attr) {
                var img = element.find("img");
                
            }
        };
    })*/
    .directive('oibHero', function ($window) {
        return {
            restrict: "E",
            scope: {
                src: "@oibSrc",
                actualHeight: "@oibActualHeight",
                actualWidth: "@oibActualWidth",
                mediaSwitchWidth: "@oibMediaSwitchWidth"
            },
            template: "<div style='margin:0px 0px;padding:0px 0px'><ng-transclude></ng-transclude></div>",
            transclude: true,
            replace: true,
            link: function (scope, element, attr) {

                element.css({
                    'background-image': 'url(' + scope.src + ')',
                    'background-size': 'cover',
                    'position': 'relative'
                });

                scope.onWindowResize = function () {
                    var width = $window.innerWidth; //Math.Max($window.innerWidth, scope.minWidth);               

                    var ratio = width / scope.actualWidth;
                    var height = scope.actualHeight * ratio;
                    element.css({
                        'height': height + "px",
                        'width': width + "px",
                    });
                };

                scope.onWindowResize();

                angular.element($window).bind('resize', function () {
                    scope.onWindowResize();
                });
            }
        };
    })
    .directive('oibFitImage', function () {
        //http://stackoverflow.com/questions/9994493/scale-image-to-fit-a-bounding-box/10016640#10016640
        return {
            restrict: "E",
            scope: {
                src: "@oibSrc",
                height: "@oibHeight",
                width: "@oibWidth"
            },
            template: "<div></div>",
            transclude: true,
            replace: true,
            link: function (scope, element, attr) {

                String.prototype.replaceAll = function (search, replacement) {
                    var target = this;
                    return target.split(search).join(replacement);
                };

                scope.encodedSrc = function () {
                    return encodeURI(scope.src)
                        .replaceAll("~", "%7E")
                        .replaceAll("!", "%21")
                        .replaceAll("*", "%2A")
                        .replaceAll("(", "%28")
                        .replaceAll(")", "%29")
                        .replaceAll(",", "%2C")
                        .replaceAll(";", "%3B")
                        .replaceAll("+", "%2B");
                };

                element.css({
                    'margin-left': '10px',
                    'margin-top': '3px',
                    'margin-right': '15px',
                    'padding': '5px',
                    'border': '1px solid #dddddd',
                    'height': scope.height,
                    'width': scope.width,
                    'background-image': "url(" + scope.encodedSrc() + ")",
                    'background-repeat': 'no-repeat',
                    'background-size': 'contain',
                    'background-position': 'center'
                });
            }
        };
    })
    .directive('oibJangoNotification', function () {
        return {
            restrict: 'EA',
            scope: {
                result: '&',
                messages: '='
            },
            template: 
            '<div ng-repeat="m in messages">' +           
                '<div class="c-cookies-bar c-cookies-bar-1 c-cookies-bar-top c-bg-red animated fadeOutUp" data-wow - delay="2s" style= "visibility: visible; animation-delay: 2s; animation-name: fadeOutUp; opacity: 1;" > ' +
                    '<div class="c-cookies-bar-container">' +
                        '<div class="row">' +
                            '<div class="col-md-9">' +
                                '<div class="c-cookies-bar-content c-font-white">' +
                                    '{{m.data}}'+
                                '</div>' +
                            '</div>' +
                            '<div class="col-md-3">' +
                                '<div class="c-cookies-bar-btn" >' +
                                    '<a ng-click="result()" class="c-cookies-bar-close btn c-btn-white c-btn-border-1x c-btn-bold c-btn-square c-cookie-bar-link">Close</a>' +                                       
                                '</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' + 
            '</div>',            
            replace: true,            
            link: function (scope, element, attrs) { 
                
            }
        };
    }).directive('oibCheckbox', function () {
        return {
            restrict: 'EA',
            scope: {
                result: '&',
                messages: '='
            },
            template:
                '<label for="checkbox2-88">' +
                '<span class="inc"></span>' +
                '<span class="check"></span>' +
                '<span class="box"></span>' +
                'Checked Option</label>',
            replace: true,
            link: function (scope, element, attrs) {

            }
        };
    })
    .directive('switch', function () {
        return {
            restrict: 'AE'
            , replace: true
            , transclude: true
            , template: function (element, attrs) {
                var html = '';
                html += '<span';
                html += ' class="switch' + (attrs.class ? ' ' + attrs.class : '') + '"';
                html += attrs.ngModel ? ' ng-click="' + attrs.disabled + ' ? ' + attrs.ngModel + ' : ' + attrs.ngModel + '=!' + attrs.ngModel + (attrs.ngChange ? '; ' + attrs.ngChange + '()"' : '"') : '';
                html += ' ng-class="{ checked:' + attrs.ngModel + ', disabled:' + attrs.disabled + ' }"';
                html += '>';
                html += '<small></small>';
                html += '<input type="checkbox"';
                html += attrs.id ? ' id="' + attrs.id + '"' : '';
                html += attrs.name ? ' name="' + attrs.name + '"' : '';
                html += attrs.ngModel ? ' ng-model="' + attrs.ngModel + '"' : '';
                html += ' style="display:none" />';
                html += '<span class="switch-text">'; /*adding new container for switch text*/
                html += attrs.on ? '<span class="on">' + attrs.on + '</span>' : ''; /*switch text on value set by user in directive html markup*/
                html += attrs.off ? '<span class="off">' + attrs.off + '</span>' : ' ';  /*switch text off value set by user in directive html markup*/
                html += '</span>';
                return html;
            }
        }
    });