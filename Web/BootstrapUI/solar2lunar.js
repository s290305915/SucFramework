var Holiday =  {
	lunarInfo: [
		0x04bd8, 0x04ae0, 0x0a570, 0x054d5, 0x0d260, 0x0d950, 0x16554, 0x056a0, 0x09ad0, 0x055d2, 0x04ae0, 0x0a5b6, 0x0a4d0, 0x0d250, 0x1d255, 0x0b540, 0x0d6a0,  
		0x0ada2, 0x095b0, 0x14977, 0x04970, 0x0a4b0, 0x0b4b5, 0x06a50, 0x06d40, 0x1ab54, 0x02b60, 0x09570, 0x052f2, 0x04970, 0x06566, 0x0d4a0, 0x0ea50, 0x06e95, 
		0x05ad0, 0x02b60, 0x186e3, 0x092e0, 0x1c8d7, 0x0c950, 0x0d4a0, 0x1d8a6, 0x0b550, 0x056a0, 0x1a5b4, 0x025d0, 0x092d0, 0x0d2b2, 0x0a950, 0x0b557, 0x06ca0, 
		0x0b550, 0x15355, 0x04da0, 0x0a5d0, 0x14573, 0x052d0, 0x0a9a8, 0x0e950, 0x06aa0, 0x0aea6, 0x0ab50, 0x04b60, 0x0aae4, 0x0a570, 0x05260, 0x0f263, 0x0d950, 
		0x05b57, 0x056a0, 0x096d0, 0x04dd5, 0x04ad0, 0x0a4d0, 0x0d4d4, 0x0d250, 0x0d558, 0x0b540, 0x0b5a0, 0x195a6, 0x095b0, 0x049b0, 0x0a974, 0x0a4b0, 0x0b27a, 
		0x06a50, 0x06d40, 0x0af46, 0x0ab60, 0x09570, 0x04af5, 0x04970, 0x064b0, 0x074a3, 0x0ea50, 0x06b58, 0x055c0, 0x0ab60, 0x096d5, 0x092e0, 0x0c960, 0x0d954, 
		0x0d4a0, 0x0da50, 0x07552, 0x056a0, 0x0abb7, 0x025d0, 0x092d0, 0x0cab5, 0x0a950, 0x0b4a0, 0x0baa4, 0x0ad50, 0x055d9, 0x04ba0, 0x0a5b0, 0x15176, 0x052b0, 
		0x0a930, 0x07954, 0x06aa0, 0x0ad50, 0x05b52, 0x04b60, 0x0a6e6, 0x0a4e0, 0x0d260, 0x0ea65, 0x0d530, 0x05aa0, 0x076a3, 0x096d0, 0x04bd7, 0x04ad0, 0x0a4d0, 
		0x1d0b6, 0x0d250, 0x0d520, 0x0dd45, 0x0b5a0, 0x056d0, 0x055b2, 0x049b0, 0x0a577, 0x0a4b0, 0x0aa50, 0x1b255, 0x06d20, 0x0ada0,//2049
		0x14b63,0x09370,0x049f8,0x04970,0x064b0,0x168a6,0x0ea50, 0x06b20,0x1a6c4,0x0aae0,//2050-2059
		0x0a2e0,0x0d2e3,0x0c960,0x0d557,0x0d4a0,0x0da50,0x05d55,0x056a0,0x0a6d0,0x055d4,//2060-2069
		0x052d0,0x0a9b8,0x0a950,0x0b4a0,0x0b6a6,0x0ad50,0x055a0,0x0aba4,0x0a5b0,0x052b0,//2070-2079
		0x0b273,0x06930,0x07337,0x06aa0,0x0ad50,0x14b55,0x04b60,0x0a570,0x054e4,0x0d160,//2080-2089
		0x0e968,0x0d520,0x0daa0,0x16aa6,0x056d0,0x04ae0,0x0a9d4,0x0a2d0,0x0d150,0x0f252,//2090-2099
		0x0d520,//2100
	],
	sTermInfo: [0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758],
	solarTerm: new Array("小寒","大寒","立春","雨水","惊蛰","春分","清明","谷雨","立夏","小满","芒种","夏至","小暑","大暑","立秋","处暑","白露","秋分","寒露","霜降","立冬","小雪","大雪","冬至"),
	lFtv: new Array("0101*春节", "0115 元宵", "0505 端午", "0707 七夕", "0715 中元节", "0815 中秋", "0909 重阳", "1208 腊八", "1223 小年", "0100*除夕"),
	sFtv: new Array("0101*元旦", "0214 情人", "0308 妇女节", "0401 愚人", "0501 劳动", "0504 青年", "0601 儿童", "0801 建军", "0910 教师", "1001*国庆", "1224 平安", "1225 圣诞"),
	solarMonth: new Array(31,28,31,30,31,30,31,31,30,31,30,31),
	solarWeek: new Array('日','一','二','三','四','五','六','七','八','九','十'),
	Lunar: function(objDate) {
		var returndata = {};
		var i, leap = 0,
			temp = 0,sY = objDate.getFullYear(),sM = objDate.getMonth()
		var baseDate = new Date(1900, 0, 31)
		var offset = (objDate - baseDate) / 86400000
		returndata.dayCyl = offset + 40
		returndata.monCyl = 14
		for (i = 1900; i < 2101 && offset > 0; i++) {
			temp = this.lYearDays(i)
			offset -= temp
			returndata.monCyl += 12
		}
		if (offset < 0) {
			offset += temp;
			i--;
			returndata.monCyl -= 12
		}
		returndata.year = i
		returndata.yearCyl = i - 1864
		leap = this.leapMonth(i)
		returndata.isLeap = false
		for (i = 1; i < 13 && offset > 0; i++) {
			if (leap > 0 && i == (leap + 1) && returndata.isLeap == false) {
				--i;
				returndata.isLeap = true;
				temp = this.leapDays(returndata.year);
			} else {
				temp = this.monthDays(returndata.year, i);
			}
			if (returndata.isLeap == true && i == (leap + 1)) returndata.isLeap = false
			offset -= temp
			if (returndata.isLeap == false) returndata.monCyl++
		} 
		if (offset == 0 && leap > 0 && i == leap + 1) if (returndata.isLeap) {
			returndata.isLeap = false;
		} else {
			returndata.isLeap = true;
			--i;
			--returndata.monCyl;
		}
		if (offset < 0) {
			offset += temp;
			--i;
			--returndata.monCyl;
		}
		returndata.month = i
		returndata.day = offset + 1 
		returndata.days = this.solarDays(sY,sM)
		return returndata;
	},
	getholiday: function(date) {
		var sDObj = this.strtotime(date);
		var lDObj = this.Lunar(sDObj);
		var lDPOS = new Array(3); 
		var sDobj = {};
		var SY = sDObj.getFullYear();
		var SM = sDObj.getMonth();
		var SD = sDObj.getDate();
		var festival = '',
			solarTerms = '',
			solarFestival = '',
			lunarFestival = '',
			tmp1, tmp2;
		var lFtv = this.lFtv;
		var sFtv = this.sFtv;
		for (i in lFtv)
		if (typeof(lFtv[i].match) == 'function' && lFtv[i].match(/^(\d{2})(.{2})([\s\*])(.+)$/)) {
			tmp1 = Number(RegExp.$1) - lDObj.month
			tmp2 = Number(RegExp.$2) - parseInt(lDObj.day)
			if (tmp1 == 0 && tmp2 == 0) lunarFestival = RegExp.$4
		}
		for (i in sFtv)
		if (typeof(sFtv[i].match) == 'function' && sFtv[i].match(/^(\d{2})(\d{2})([\s\*])(.+)$/)) {
			tmp1 = Number(RegExp.$1) - (SM + 1)
			tmp2 = Number(RegExp.$2) - SD
			if (tmp1 == 0 && tmp2 == 0) solarFestival = RegExp.$4
		}
		tmp1 = new Date((31556925974.7 * (SY - 1900) + this.sTermInfo[SM * 2 + 1] * 60000) + Date.UTC(1900, 0, 6, 2, 5))
		tmp2 = tmp1.getUTCDate()
		if (tmp2 == SD) solarTerms = this.solarTerm[SM * 2 + 1]
		tmp1 = new Date((31556925974.7 * (SY - 1900) + this.sTermInfo[SM * 2] * 60000) + Date.UTC(1900, 0, 6, 2, 5))
		tmp2 = tmp1.getUTCDate()
		if (tmp2 == SD) solarTerms = this.solarTerm[SM * 2]
		if (solarTerms == '' && solarFestival == '' && lunarFestival == '') festival = '';
		else festival = solarTerms + solarFestival + lunarFestival;
		var cl = '<font color="#000066" STYLE="font-size:9pt;">'; 
		sDobj.date = date;
		sDobj.days = this.solarDays(SY,SM)
		sDobj.week = this.solarWeek[sDObj.getDay()]
		return [festival,lDObj,sDobj];
	},
	lYearDays: function(y) {
		var i, sum = 348
		for (i = 0x8000; i > 0x8; i >>= 1)
		sum += (this.lunarInfo[y - 1900] & i) ? 1 : 0
		return (sum + this.leapDays(y))
	},
	leapDays: function(y) {
		if (this.leapMonth(y)) return ((this.lunarInfo[y - 1900] & 0x10000) ? 30 : 29)
		else return (0)
	},
	leapMonth: function(y) {
		return (this.lunarInfo[y - 1900] & 0xf)
	},
	monthDays: function(y, m) {
		return ((this.lunarInfo[y - 1900] & (0x10000 >> m)) ? 30 : 29)
	},
	getrelaxesday: function(timestr) {
		var list = {
			"2016": ["2016-01-01", "2016-01-02", "2016-01-03", "2016-02-07", "2016-02-08", "2016-02-09", "2016-02-10", "2016-02-11", "2016-02-12", "2016-02-13", "2016-04-02", "2016-04-03", "2016-04-04", "2016-04-30", "2016-05-01", "2016-05-02", "2016-06-09", "2016-06-10", "2016-06-11", "2016-09-15", "2016-09-16", "2016-09-17", "2016-10-01", "2016-10-02", "2016-10-03", "2016-10-04", "2016-10-05", "2016-10-06", "2016-10-07"]
		};
		var d = new Date(Date.parse(timestr.replace(/-/g, "/")));
		var today_year = d.getFullYear();
		timestr = this.timeformat(d);
		var data = list[today_year];
		if (this.isEmpty(data)) {
			return '';
		}
		var isholiday = 0;
		for (var i in data) {
			var date = data[i];
			if (date == timestr) {
				isholiday = 1;
			}
		}
		if (isholiday == 1) {
			return '休';
		}
		return '';
	},
	strtotime: function(timestr) { 
		var d = new Date(Date.parse(timestr.replace(/-/g, "/"))); 
		return d;
	},
	timeformat: function(date, format) {
		if (this.isEmpty(format)) {
			format = "Y-m-d";
		}
		var year = date.getFullYear();
		var month = this.zerosize(date.getMonth() + 1 + "", 2);
		var day = this.zerosize(date.getDate() + "", 2);
		var nmonth = this.zerosize(date.getMonth() + 1 + "", 1);
		var jday = this.zerosize(date.getDate() + "", 1);
		var hours = this.zerosize(date.getHours());
		var minis = this.zerosize(date.getMinutes());
		var second = this.zerosize(date.getSeconds());
		var time = format.replace("Y", year).replace("m", month).replace("d", day).replace("n", nmonth).replace("j", jday).replace("h", hours).replace("i", minis).replace("s", second);
		return time;
	},
	zerosize: function(value, length) {
		if (!length) length = 2;
		value = String(value);
		for (var i = 0, zeros = ''; i < (length - value.length); i++) {
			zeros += '0';
		}
		return zeros + value;
	},
	isEmpty: function(str) {
		return str == undefined || str == null || str == '';
	},
	//返回公历y年m+1月的天数
	solarDays: function(y,m){
	   if(m==1)
	      return(((y%4==0)&&(y%100!=0)||(y%400==0))?29:28);//闰年28天
	   else
	      return(this.solarMonth[m]);
	}

};
 
//var dtObj = Holiday.getholiday('2017-10-01');

//console.log(dtObj[0]);
//console.log(dtObj[1]);
//console.log(dtObj[2]);