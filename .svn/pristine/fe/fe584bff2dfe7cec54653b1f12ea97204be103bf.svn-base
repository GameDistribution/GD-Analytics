/*!
 * CheckedList v1.0.0 (http://www.gamedistribution.com)
 * Copyright 2011-2015 GameDistribution, Inc.
 * Author Reha Bicer astronbnx@gmail.com
 * Licensed under GPL.  
 */

if (typeof jQuery === 'undefined') {
    throw new Error('CheckedList\'s JavaScript requires jQuery')
}

+function ($) {

    var CheckedList = function (element, options) {
        this.$element = $(element)
        this.options = $.extend({}, CheckedList.DEFAULTS, options)
        this.settings = {
            on: {
                icon: 'glyphicon glyphicon-check'
            },
            off: {
                icon: 'glyphicon glyphicon-unchecked'
            }
        };
        this.init();
    }

    CheckedList.VERSION = '1.0.0'

    CheckedList.DEFAULTS = {
        loadingText: 'loading...',
    }

    CheckedList.prototype.updateDisplay = function ($widget, $checkbox) {
        var isChecked = $checkbox.is(':checked');

        // Set the button's state
        $widget.data('state', (isChecked) ? "on" : "off");

        // Set the button's icon
        $widget.find('.state-icon')
            .removeClass()
            .addClass('state-icon ' + this.settings[$widget.data('state')].icon);

        // Update the button's color
        if (isChecked) {
            $widget.addClass($widget.style + $widget.color + ' active');
        } else {
            $widget.removeClass($widget.style + $widget.color + ' active');
        }
    }

    CheckedList.prototype.selectAll = function () {
        var $this = this;
        $($this.$element.context.children).each(function () {
            var $widget = $(this);
            $widget.triggerHandler('click',true);
        });
    }

    CheckedList.prototype.deselectAll = function () {
        var $this = this;
        $($this.$element.context.children).each(function () {
            var $widget = $(this);
            $widget.triggerHandler('click', false);
        });
    }

    CheckedList.prototype.selectedCount = function () {
        return $(this.$element.context.children).filter(".active").length;
    }

    CheckedList.prototype.count = function () {
        return this.$element.context.children.length;
    }

    CheckedList.prototype.selectedItems = function () {
        return $(this.$element.context.children).filter(".active");
    }

    CheckedList.prototype.init = function () {

        var $this = this;
        //'.list-group.checked-list-box .list-group-item'
        $($this.$element.context.children).each(function () {

            // Settings
            var $widget = $(this),
                $checkbox = $('<input type="checkbox" class="hidden" />');
            $widget.color = ($widget.data('color') ? $widget.data('color') : "primary"),
            $widget.style = ($widget.data('style') == "button" ? "btn-" : "list-group-item-");

            $widget.css('cursor', 'pointer')
            $checkbox.prop('value', $widget.context.value);
            $widget.append($checkbox);

            // Event Handlers
            $widget.on('click', function (e,state) {
                e.stopPropagation();
                if (typeof state === "undefined") {
                    $checkbox.prop('checked', !$checkbox.is(':checked'));
                } else {
                    $checkbox.prop('checked', state);
                }
                $checkbox.triggerHandler('change');
                if (typeof state === "undefined") {
                    $this.$element.trigger("click.checkedList");
                }
            });
            $checkbox.on('change', function () {
                $this.updateDisplay($widget, $checkbox);
            });

            if ($widget.data('checked') == true) {
                $checkbox.prop('checked', !$checkbox.is(':checked'));
            }

            $this.updateDisplay($widget,$checkbox);

            if ($widget.find('.state-icon').length == 0) {
                $widget.prepend('<span class="state-icon ' + $this.settings[$widget.data('state')].icon + '"></span>');
            }
        });
    }

    // CHECKEDLIST PLUGIN DEFINITION
    // ========================
    function Plugin(option) {
        switch (option) {
            case 'count': return $(this).data('astron.checkedlist').count();
            case 'selectedCount': return $(this).data('astron.checkedlist').selectedCount();
            case 'selectedItems': return $(this).data('astron.checkedlist').selectedItems();
            default: return this.each(function () {
                var $this = $(this)
                var data = $this.data('astron.checkedlist')
                var options = typeof option == 'object' && option

                $this.data('astron.checkedlist', (data = new CheckedList(this, options)));
                if (typeof option == 'string') data[option]()
            })
        }
    }

    var old = $.fn.checkedList;

    $.fn.checkedList = Plugin;
    $.fn.checkedList.Constructor = CheckedList;

    // CHECKEDLIST NO CONFLICT
    // ==================
    $.fn.checkedList.noConflict = function () {
        $.fn.checkedList = old;
        return this
    }

}(jQuery);
