<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlPager.ascx.cs" Inherits="analytics_ctrlPager" %>
            <div class="row-fluid">
                <div class="col-md-12 text-right">
                    <ul class="pagination" id="<%=PagerName %>" totalPage="0" currentPage="0" pageLimit="0">
                        <li><a href="#"><span id="pageNumber">Page 1/1</span></a></li>
                        <li><a href="#" class="pagerPre">&larr;</a></li>
                        <li><a href="#" class="pagerNext">&rarr;</a></li>
                    </ul>
                </div>
            </div>
