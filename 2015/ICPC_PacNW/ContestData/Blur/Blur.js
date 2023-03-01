process.stdin.setEncoding('utf8');

var allinput = new Array() ;
var at = 0 ;

process.stdin.on('readable', function() {
  var chunk = process.stdin.read();
  if (chunk !== null) {
    allinput.push(chunk) ;
  }
});

process.stdin.on('end', function() {
  allinput = allinput.join('').split("\n") ;
  at = 0 ;
  doproblem() ;
});
function getline() {
   if (at >= allinput.length)
      return null ;
   return allinput[at++] ;
}
function doproblem() {
   var nums = getline().split(" ") ;
   var w = nums[0]-0 ;
   var h = nums[1]-0 ;
   var g = nums[2]-0 ;
   var b = [] ;
   for (var i=0; i<h; i++) {
      var dat = getline().split(" ") ;
      var lin = [] ;
      for (var j=0; j<w; j++) {
         lin.push(dat[j]-0) ;
      }
      b.push(lin) ;
   }
   for (var gg=0; gg<g; gg++) {
      var bb = [] ;
      for (var i=0; i<h; i++) {
         var lin = [] ;
         for (var j=0; j<w; j++) {
            lin.push(b[i][j] + b[i][(j+1)%w] + b[i][(j+w-1)%w]) ;
         }
         bb.push(lin) ;
      }
      b = [] ;
      for (var i=0; i<h; i++) {
         var lin = [] ;
         for (var j=0; j<w; j++) {
            lin.push(bb[i][j] + bb[(i+1)%h][j] + bb[(i+h-1)%h][j]) ;
         }
         b.push(lin) ;
      }
   }
   var o = new Object() ;
   var r = 0 ;
   for (var i=0; i<h; i++) {
      for (var j=0; j<w; j++) {
         var key = "K" + b[i][j] ;
         if (o[key] == undefined) {
            o[key] = key ;
            r++ ;
         }
      }
   }
   process.stdout.write(r + "\n") ;
}
