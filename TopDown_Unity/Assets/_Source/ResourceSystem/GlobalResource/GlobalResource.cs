using System;

namespace ResourceSystem.GlobalResource
{
    public class GlobalResource
    {
        public event Action<int> OnChangeResourceCount;
        
        private int _resourceCount;

        public void ChangeResourceCount(int addResource)
        {
            _resourceCount += addResource;
            OnChangeResourceCount?.Invoke(_resourceCount);
        }
    }
}