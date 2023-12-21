using UlearnTodoTimer.Infrasturcture;
using VkNet.Abstractions;

namespace UlearnTodoTimer.Services;

public class VkService: IVkService
{
    public IVkApi VkApi;

    public VkService(IVkApi vkApi)
    {
        VkApi = vkApi;
    }

    public UserInfo GetBaseInfoAboutUser()
    {
        throw new NotImplementedException();
    }
}