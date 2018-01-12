Array.prototype.find = function( predicate )
{
    for (i = 0; i < this.length; i++)
        if (predicate(this[i]))
            return this[i];
    return null;
}